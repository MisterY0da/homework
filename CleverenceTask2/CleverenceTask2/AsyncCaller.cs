using System;
using System.Threading.Tasks;

namespace CleverenceTask2
{
    public class AsyncCaller
    {
        private readonly EventHandler handler;

        public AsyncCaller(EventHandler handler)
        {
            this.handler = handler;
        }

        public bool Invoke(int timeout, object sender, EventArgs e)
        {
            var task = Task.Factory.StartNew(() => handler.Invoke(sender, e));

            return task.Wait(timeout);
        }
    }
}
