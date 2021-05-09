using System;
using System.IO;

namespace Actividad_3
{
    class Program
    {
        static void Main(string[] args)
        {
         
            int index = 1;
            bool EOF = false;
            decimal debe = 0;
            decimal haber = 0;

            while (!EOF)
            {
                bool fechaCorrecta = false;
                do
                {
                    Console.WriteLine("Ingrese fecha el asiento en formato dd/mm/yyyy");
                    string fecha = Console.ReadLine();

                    fechaCorrecta = DateTime.TryParse(fecha, out DateTime fechaAsiento);
                    if (!fechaCorrecta)
                    {
                        Console.WriteLine("No se ha ingresado una fecha correcta");
                        continue;
                    }

                    if (fechaAsiento > DateTime.Now)
                    {
                        Console.WriteLine("Debe ingresar una fecha menor a la actual");
                        continue;
                    }
                    var asiento = new Asiento();
                    asiento.NroAsiento = index;
                    asiento.FechaDeAsiento = fechaAsiento;
                    asiento.guardarAsientos();

                } while (!fechaCorrecta);

                bool pedirMasCodigos = true;
                while (pedirMasCodigos)
                {
                    Console.WriteLine("Ingrese un código de cuenta");
                    string ingreso = Console.ReadLine(); //TRAER EL NUMERO DE CUENTA DESDE EL PLAN DE CUETNAS CON SU VALOR (ACITVO O PASIVO)
                    bool formatoCodigoCorrecto = int.TryParse(ingreso, out int codigoCuenta);
                    while (!formatoCodigoCorrecto)
                    {
                        Console.WriteLine("No ingresó un número entero");
                        Console.WriteLine("Ingrese un código de cuenta");
                        ingreso = Console.ReadLine(); 
                        formatoCodigoCorrecto = int.TryParse(ingreso, out codigoCuenta);
                    }

                        using (var reader = new StreamReader(@"C:\Users\Damian\source\repos\Actividad_3\planDeCuentas.txt"))
                        {
                        bool encontro = false;
                            while (!reader.EndOfStream & !encontro )
                            {
                                var linea = reader.ReadLine();
                            //Crear la variable codigoLinea que tiene que ser igual a el parse de la linea de los priemros 2 digitos y los ultimos 6
                            string[] subs = linea.Split("|");
                            string type = subs[2];
                            Console.WriteLine(subs[2]);
                            string codigoLinea = subs[0];
                            bool validarCodigo = int.TryParse(ingreso, out int codLinea);
                            //CONVERTIR CODIGO LINEA EN UN INT
                            if (codLinea == codigoCuenta)
                            {
                                encontro = true;
                                if (type == "Activo")
                                {
                                    Console.WriteLine("Ingrese el DEBE");
                                    string db = Console.ReadLine();
                                    bool validarDebe = decimal.TryParse(ingreso, out decimal debeProvisorio);
                                    //Crear nueva linea en el file diario.txt 
                                    //index | fechaAsiento | CodigoLinea | debeProvisorio | 0
                                    debe = debe + debeProvisorio;
                                    
                                } 
                                
                                else
                                {
                                    Console.WriteLine("Ingrese el HABER");
                                    string hb = Console.ReadLine();
                                    bool validarHaber = decimal.TryParse(ingreso, out decimal haberProvisorio);
                                    //Crear nueva linea en el file diario.txt 
                                    //index | fechaAsiento | CodigoLinea | 0 | haberProvisorio
                                    haber = haber + haberProvisorio;
                                    
                                }
                            }
                            else
                            {
                                Console.WriteLine("El codigo ingresado no existe");
                            }
                            }
                        }
                    Console.WriteLine("Desea continuar ingresando códigos de cuenta: SI/NO");
                    string respuesta = Console.ReadLine();
                    if (respuesta == "NO")
                    {
                        pedirMasCodigos = false;
                        index++;
                    }                    
                }

                
                
                Console.WriteLine("Desea continuar ingresando asientos? : SI/NO");
                string rta = Console.ReadLine();
                if (rta == "NO")
                {
                    EOF = true;
                }                
            }
        }
    }
}
