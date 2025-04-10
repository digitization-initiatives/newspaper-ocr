using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperOCR.src
{
    internal class TaskStatus
    {
        public Task RunningTask { get; set; }

        public DirectoryStructure Item { get; set; }

        public TaskStatus (Task runningTask, DirectoryStructure item)
        {
            RunningTask = runningTask;
            Item = item;
        }
    }
}
