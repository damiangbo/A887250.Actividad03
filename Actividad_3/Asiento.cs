using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Actividad_3
{
    class Asiento
    {
        public int NroAsiento { get; set; }
        public DateTime FechaDeAsiento { get; set; }
        public int CodigoCuenta { get; set; }
        public decimal Debe { get; set; }
        public decimal Haber { get; set; }

        public void guardarAsientos() //Que valor iría acá?
        {
            if (Debe == Haber)
            { 
            var diario = File.CreateText("Diario");
            diario.WriteLine($"{NroAsiento}|{FechaDeAsiento:yyyy-MM-dd}|{CodigoCuenta}|{Debe}|{Haber}");
            diario.Close();
            }else {
                Console.WriteLine("El debe y el haber no son iguales");
                  }
        }       
    }
}
