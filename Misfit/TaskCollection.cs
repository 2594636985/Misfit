using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Misfit.Modulation
{
    public class TaskCollection : List<TaskInfo>
    {
        public void Shutdown()
        {
            if (this.Count > 0)
            {
                for (int i = 0; i < this.Count; i++)
                {
                    TaskInfo taskInfo = this[i];
                    if (!taskInfo.CancellationTokenSource.IsCancellationRequested)
                    {
                        taskInfo.CancellationTokenSource.Cancel();
                        this.Remove(taskInfo);
                        i--;
                    }
                }
            }
        }
    }

    public class TaskInfo
    {
        public CancellationTokenSource CancellationTokenSource { private set; get; }

        public Task Task { private set; get; }

        public TaskInfo(Task task, CancellationTokenSource takenSource)
        {
            this.CancellationTokenSource = takenSource;
            this.Task = task;
        }
    }
}
