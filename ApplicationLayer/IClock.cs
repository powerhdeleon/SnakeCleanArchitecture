using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer
{
    public interface IClock
    {
        IDisposable Start(TimeSpan interval, Action onTick);
    }
}
