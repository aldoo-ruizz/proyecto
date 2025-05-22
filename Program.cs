using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace proyecto
{
    internal class Program
    {
            public enum menu
        {
            consultar = 1, depositar, retirar, revisard, revisarr, salir

        }
        static double saldo = 0;
        static Dictionary<DateTime, double> deposito = new Dictionary<DateTime, double>();
        static Dictionary<DateTime, double> retiro = new Dictionary<DateTime, double>();
        static void Main(string[] args)
        {
            int intentos = 3;
            do
            {
                if (loggin())
                {
                    while (true)
                    {
                        switch (men())
                        {
                            case menu.consultar:
                                Console.WriteLine(consultar());
                                break;
                            case menu.depositar:
                                depositar();
                                break;
                                case menu.retirar:
                                retirar();
                                break;
                            case menu.revisard:
                                revisardepo();
                                break;
                                case menu.revisarr:
                                revisarretiros();
                                break;
                            case menu.salir:
                                Environment.Exit(0);
                                break;
                     
                        }
                    }
                }
                else
                {
                    intentos--;
                    Console.WriteLine($"Te quedan: {intentos} intentos");

                }
            }
            while (intentos >= 1);
        }
        static menu men()
        {
            Console.WriteLine("1)Consultar saldo actual.");
            Console.WriteLine("2)Depositar dinero.");
            Console.WriteLine("3)Retirar dinero (solo si el saldo es suficiente).");
            Console.WriteLine("4)Revisar historial de depósitos ");
            Console.WriteLine("5)Revisar historial de retiros");
            Console.WriteLine("6)Salir");
            menu opc = (menu)Convert.ToInt32(Console.ReadLine());
            return opc;
        }


        static double consultar()
        {
           return saldo;
        }
      

        static void depositar()
        {
            Console.WriteLine("cuanto deseas depositar?");
            double depo=Convert.ToDouble(Console.ReadLine());
            saldo += depo;
            deposito.Add(DateTime.Now, depo);
        }
        static bool loggin()
        {

            DateTime fechaactual = DateTime.Now;
            Console.WriteLine($"Dame el usuario:");
            string usuario = Console.ReadLine();
            Console.WriteLine($"Dame contraseña:");
            string contrasena = Console.ReadLine();
            Console.WriteLine($"Dame fecha de nacimiento en formato dd/MM/yyyy:");
            DateTime fechanac = Convert.ToDateTime(Console.ReadLine());
            int edad = fechaactual.Year - fechanac.Year;

            if (usuario == "Aldo" && contrasena == "123" && edad >= 18)
                return true;
            else
                return false;
        }
        static void retirar()
        {
            Console.WriteLine($"¿Cuánto deseas retirar?");
            double retirar = double.Parse(Console.ReadLine());
            if (saldo >= retirar)
            {
                saldo -= retirar;
                retiro.Add(DateTime.Now, retirar);
                Console.WriteLine($"Has retirado {retirar} y tu saldo actual es {saldo}");
            }
            else
            {
                Console.WriteLine("Fondos insuficientes");
            }

        }
        static void revisardepo()
        {
            Console.WriteLine("Depositos ");
            foreach (var d in deposito)
            {
                Console.WriteLine($"fecha de deposito: {d.Key} cantidad: {d.Value}");
            }
            Console.WriteLine("Deseas enviar por correo el historial de depositos?");
            Console.WriteLine("1)Si");
            Console.WriteLine("2)No");

            int opc = Convert.ToInt32(Console.ReadLine());
            if (opc==1)
            {
                enviarcorreo(deposito);
            }
            else
            {
                Console.WriteLine("Regresando al menu");
            }
        }
        static void revisarretiros()
        {
            Console.WriteLine("Retiros ");
            foreach (var d in retiro)
            {
                Console.WriteLine($"fecha de deposito: {d.Key} cantidad: {d.Value}");
            }
            Console.WriteLine("Deseas enviar por correo el historial de depositos?");
            Console.WriteLine("1)Si");
            Console.WriteLine("2)No");

            int opc = Convert.ToInt32(Console.ReadLine());
            if (opc == 1)
            {
                enviarcorreo(retiro);
            }
            else
            {
                Console.WriteLine("Regresando al menu");
            }
        }
        static bool enviarcorreo(Dictionary<DateTime, double> transacciones)
        {
            string servidorSmtp = "smtp.office365.com";
            int puerto = 587;
            string usuario = "113512@alumnouninter.mx";  
            string contrasena = "Zafiro17";  

          
            SmtpClient smtp = new SmtpClient(servidorSmtp)
            {
                Port = puerto,
                Credentials = new NetworkCredential(usuario, contrasena),
                EnableSsl = true
            };
           
            MailMessage mail = new MailMessage
            {
                From = new MailAddress(usuario),  
                Subject = "Transacciones",   
                IsBodyHtml = false 
            };

           
            string cuerpoMensaje = "Las tareas transacciones son:\n\n";
            foreach (var c in transacciones)
            {
                cuerpoMensaje += $"{c.Key}. {c.Value}\n";
            }
            mail.Body = cuerpoMensaje;  

            mail.To.Add("aldoruiz2006@gmail.com"); 

            smtp.Send(mail);
            return true;
        }


    }
}
    

