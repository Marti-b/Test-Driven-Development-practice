using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tests
{
    [TestFixture]
    class UpdateableSpinTests
    {
        [Test]
        public void Wait_NoPulse_ReturnsFalse()
        {
            UpdateableSpin spin = new UpdateableSpin();
            bool wasPulsed = spin.Wait(TimeSpan.FromMilliseconds(10));
            Assert.IsFalse(wasPulsed);
        }

        [Test]
        public void Wait_Pulse_ReturnsTrue()
        {
            UpdateableSpin spin = new UpdateableSpin();

            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(100);
                spin.Set();
            });
            bool wasPulsed = spin.Wait(TimeSpan.FromSeconds(10));
            Assert.IsTrue(wasPulsed);
        }

        [Test]
        public void Wait50Milisec_CallIsActuallyWaitingFor50Milisec()
        {
            var spin = new UpdateableSpin();

            Stopwatch watcher = new Stopwatch();
            watcher.Start();

            spin.Wait(TimeSpan.FromMilliseconds(50));

            watcher.Stop();

            TimeSpan actual = TimeSpan.FromMilliseconds(watcher.ElapsedMilliseconds);
            TimeSpan leftEpsilon = TimeSpan.FromMilliseconds(50 - (50 * 0.1));
            TimeSpan rightEpsilon = TimeSpan.FromMilliseconds(50 + (50 * 0.1));

            Assert.IsTrue(actual > leftEpsilon && actual < rightEpsilon);
        }

        public class UpdateableSpin
        {
            private readonly object lockObj = new object();
            private bool shouldWait = true;

            public bool Wait(TimeSpan timeout)
            {
                Thread.Sleep(timeout);
                if (!shouldWait)
                {
                    return true;
                }
                return false;
            }
            public void Set()
            {
                lock (lockObj)
                {
                    shouldWait = false;
                }
            }
        }
    }
}
