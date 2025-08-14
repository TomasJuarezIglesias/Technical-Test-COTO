using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Constants
{
    public static class HorarioConstants
    {
        public static readonly TimeSpan HoraApertura = new TimeSpan(9, 0, 0);
        public static readonly TimeSpan HoraCierre = new TimeSpan(18, 0, 0);
        public static readonly TimeSpan MargenEntreReservas = new TimeSpan(0, 30, 0);
    }
}
