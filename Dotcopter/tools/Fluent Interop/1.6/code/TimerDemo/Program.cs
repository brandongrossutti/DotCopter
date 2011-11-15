using System.Runtime.InteropServices;
using System.Threading;
using FluentInterop.CodeGeneration;
using FluentInterop.Expressions;
using Kosak.SimpleInterop;
using Microsoft.SPOT;
using SecretLabs.NETMF.Hardware.Netduino;

namespace TimerDemo
{
    public class Program
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct Test
        {
            public int data;
        }

        public static void Main()
        {
            Test test1;
            Test test2;

            int out1;
            int out2;

            var code = LukeCode();
            unsafe
            {
                Test* tp1 = &test1;
                Test* tp2 = &test2;
                code.Invoke((int) tp1, (int) tp2);
                out1 = *((int*)tp1);
                out2 = *((int*)tp2);
            }

            Debug.Print("out1: " + out1);
            Debug.Print("out2: " + out2);

            //var code = BuildCode();
            //new Thread(() => BlinkLedFor20Seconds(code)).Start();

            ////count to 30 in main thread
            //for (var i = 0; i < 30; ++i)
            //{
            //    Debug.Print("i=" + i);
            //    Thread.Sleep(1000);
            //}
        }

        private static void BlinkLedFor20Seconds(CompiledCode code)
        {
            //Because the code will be running as an interrupt, we need to keep the array pinned
            unsafe
            {
                fixed (short* fix = code.OpCodes)
                {
                    code.Invoke(500000, (int)Pins.ONBOARD_LED); //on for 1/2 second, off for 1/2 second
                    Thread.Sleep(20 * 1000); //sleep for 20 seconds
                    code.Invoke(0); //abort timer so we can safely exit this fixed block

                }
            }
        }

        private static CompiledCode LukeCode()
        {
            return CodeGenerator.Compile(g =>
            {
                var vector1Pointer = g.Declare.Static.IntPointer("vector1Address");
                var vector2Pointer = g.Declare.Static.IntPointer("vector2Address");

                g.Declare.Function("main", f =>
                {
                    var args = f.StandardArgs;
                    vector1Pointer = (IntPointerVariable)args.intParam0.AsIntPointer();
                    vector2Pointer = (IntPointerVariable)args.intParam1.AsIntPointer();
                    vector1Pointer.Value = vector1Pointer;
                    vector2Pointer.Value = vector2Pointer;
                });
            });

        }

        private static CompiledCode BuildCode()
        {
            return CodeGenerator.Compile(g =>
            {
                //----------------------------------------------------------------------
                //static variables shared by both the setup routine and the interrupt service routine
                //----------------------------------------------------------------------

                //delay between interrupts in microseconds
                var delayBetweenInterrupts = g.Declare.Static.Int("delayBetweenInterrupts");

                //current led status (on or off)
                var ledStatus = g.Declare.Static.Int("ledStatus");

                //stashed copy of the interop method dispatch table (the interrupt service routine needs this)
                var firmware = g.Declare.Static.MethodDispatchTable("firmware");

                //the output port that we will turn on and off
                var port = g.Declare.Static.FastOutputPort("port");

                //the so-called "HAL Completion" that we use for our timer
                var hal = g.Declare.Static.HalCompletion("hal");

                //forward declaration of the interrupt status routine (body defined below)
                var interruptServiceRoutine = g.Declare.Function("interruptServiceRoutine");

                //----------------------------------------------------------------------
                //main(delayInMicroseconds, pinNumber)
                //
                //if delayInMicroseconds>0, then initialize the output port and the timer and enqueue the timer
                //  otherwise, dequeue the timer
                //----------------------------------------------------------------------
                g.Declare.Function("main", f =>
                {
                    //gather function arguments
                    var args = f.StandardArgs;
                    var delayInMicroseconds = args.intParam0;
                    var pinNumber = args.intParam1;
                    var firmwareParam = args.firmwareParam16;

                    //delayInMicroseconds>0 means initialize, otherwise dequeue.
                    f.If(delayInMicroseconds > 0)
                      .Then(() =>
                      {
                          delayBetweenInterrupts.Value = delayInMicroseconds;
                          ledStatus.Value = 0;
                          firmware.Value = firmwareParam;

                          port.Initialize(firmware, pinNumber, false);
                          hal.InitializeForISR(firmware, interruptServiceRoutine);

                          //enqueue for the first time
                          hal.EnqueueDelta(firmware, delayInMicroseconds);
                      })
                      .Else(() =>
                      {
                          hal.Abort(firmware);
                      })
                      .Endif();
                });

                //----------------------------------------------------------------------
                //interruptServiceRoutine()
                //----------------------------------------------------------------------
                interruptServiceRoutine.Define(f =>
                {
                    ledStatus.Value = 1 - ledStatus;
                    port.Write(ledStatus);

                    //re-enqueue
                    hal.EnqueueDelta(firmware, delayBetweenInterrupts);
                });
            });
        }
    }
}
