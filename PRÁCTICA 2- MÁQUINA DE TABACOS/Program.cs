using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRÁCTICA_2__MÁQUINA_DE_TABACOS
{
    class Program
    {
        static void Main(string[] args)
        {
            //Declaración de constantes
            const String ENCABEZADO = "\t*************************\n"
                                    + "\t*                       *\n"
                                    + "\t*         TABACO        *\n"
                                    + "\t*                       *\n"
                                    + "\t*************************\n";
            //Declaracion de variables
            float[] precio = { 4.85F, 4.00F, 4.95f, 4.00F, 4.00f, 4.15F };
            String[] nombre = { "Magic", "Clock", "Caomuflage", "Italian", "Allure", "Alonso" };
            float dineroIntroducido = 0.0f;
            Boolean esCorrecto = false;
            int opcion = 0;
            String salida = "";

            //Entrada y validación
            do
            {
                Console.Clear();
                Console.WriteLine(ENCABEZADO);
                do
                {
                    
                    dineroIntroducido = leerFloatMayorDeCero();
                    if (dineroIntroducido >= 4.0F&&dineroIntroducido<=50)
                    {
                        esCorrecto = true;
                    }
                    else if (dineroIntroducido < 4.0F)
                    {
                        esCorrecto = false;
                        Console.Clear();
                        Console.WriteLine(ENCABEZADO);
                        Console.WriteLine("[ERROR] No se puede introducir una cantidad inferior a 4 Eur\n"
                                        + "Se le devolvera la cantidad introducida.");
                    }
                    else
                    {
                        esCorrecto = false;
                        Console.Clear();
                        Console.WriteLine(ENCABEZADO);
                        Console.WriteLine("[ERROR] No se puede introducir una cantidad superior a 50 Eur\n"
                                        + "Se le devolvera la cantidad introducida.");
                    }
                } while (!esCorrecto);
                Console.Clear();
                Console.WriteLine(ENCABEZADO);
                Console.WriteLine("\nListado de cajetillas:");
                opcion = mostrarMenu(nombre, precio);
                Console.WriteLine(opcion);
                esCorrecto = (dineroIntroducido < precio[opcion - 1]) ? false : true;
                if (dineroIntroducido < precio[opcion - 1])
                {
                    
                    Console.Clear();
                    Console.WriteLine(ENCABEZADO);
                    Console.WriteLine("[ERROR] Cantidad Insuficiente\n"
                                        + "Se le devolvera la cantidad introducida.");
                    Console.WriteLine("Pulsa intro para continuar.");
                    Console.ReadLine();
                }
            }while(!esCorrecto);
            //Proceso
            salida += ENCABEZADO + "\n";
            salida += "\nSu Cambio:\n";
            salida += cambio(dineroIntroducido, precio[opcion - 1])+"\n";
            salida += "".PadRight(70, '-') + "\n";
            salida += "Aquí tiene su tabaco, gracias\n";
            salida += "Su tabaco: " + nombre[opcion - 1]+"\n";
            //Salida
            Console.Clear();
            Console.Write(salida);

        }
        public static int mostrarMenu(String[] nombres, float[] precios)
        {
            int aux = 0;//La utilizaremos para usarla de indice
            int i = 0;
            Boolean esCorrecto = false;
            if (nombres.Length != precios.Length)
            {
                aux = -1;
            }
            else
            {
                for (i = 0; i < nombres.Length; i++)
                {
                    Console.WriteLine((i + 1) + " " + nombres[i].PadRight(25, '·') + (String)(precios[i] + "").PadRight(6, ' ') + " Eur");
                }
                do
                {
                    Console.Write("Selecciona un producto: ");
                    aux = leerEnterosMayorDeCero();
                    esCorrecto = (aux <= nombres.Length) ? true : false;
                    if (!esCorrecto) Console.WriteLine("[ERROR] No ha introducido una opción valida.\n"
                        + "Por favor vuelve a introducir una opción.");
                } while (!esCorrecto);

            }
            return aux;
        }
        public static int leerEnterosMayorDeCero()
        {
            String aux;
            int numero = 0;
            bool esCorrecto = false;

            do
            {
                aux = Console.ReadLine();
                esCorrecto = Int32.TryParse(aux, out numero);
                if (esCorrecto == false || numero < 0)
                {
                    Console.WriteLine("Error: no ha introducido un número o no es valido.");
                    esCorrecto = false;

                }

            } while (esCorrecto == false);
            return numero;

        }
        public static float leerFloatMayorDeCero()
        {
            String aux;
            float numero = 0.0f;
            bool esCorrecto = false;

            do
            {
                Console.Write("Por favor, introduce el dinero para comprar el tabaco: ");
                aux = Console.ReadLine();
                esCorrecto = float.TryParse(aux, out numero);
                if (esCorrecto == false || numero < 0)
                {
                    Console.WriteLine("Error: no ha introducido un número o no es valido.");
                    esCorrecto = false;

                }

            } while (esCorrecto == false);
            return numero;

        }
 
        public static string cambio(float dinero, float precio)
        {

            string aux = "";
            String[] nombres = { "Monedas de 0.05", "Monedas de 0.1", "Monedas de 0.2",  "Monedas de 0.5", "Monedas de 1", "Monedas de 2", "Billetes de 5", "Billetes de 10", "Billetes de 20", "Billetes de 50" };
            float[] monedas = { 0.05f, 0.1f, 0.2f, 0.5f, 1.0f, 2.0f, 5.0f, 10.0f, 20.0f, 50.0f };//Las monedas van ordenadas en orden ascendente
            int[] vuelta =new int [monedas.Length];
            int i = 0;
            
            dinero = dinero - precio;
            for (i = monedas.Length-1; i >=0; i--)
            {
                while (dinero >= monedas[i])
                {
                    vuelta[i]++;
                    dinero = dinero - monedas[i];
                }
                aux = aux + vuelta[i] + "\t"+nombres[i]+" Eur\n";
            }
            return aux;
        }
    }
}
