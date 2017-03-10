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
            #region Declaración de constantes
            //Este encabezado lo usaremos para la presentacion de la maquina
            const String ENCABEZADO = "\t*************************\n"
                                    + "\t*                       *\n"
                                    + "\t*         TABACO        *\n"
                                    + "\t*                       *\n"
                                    + "\t*************************\n";
            #endregion

            #region Declaracion de variables
            //En esta variable guardamos los precios de los tabacos que usaremos para las diferentes marcas
            float[] precio = { 4.85F, 4.00F, 4.95f, 4.00F, 4.00f, 4.15F };
            //En esta variable guardaremos los nombres de los tabacos ordenados de la misma manera que se han ordenado anteriormente los precios
            String[] nombre = { "Magic", "Clock", "Caomuflage", "Italian", "Allure", "Alonso" };
            float dineroIntroducido = 0.0f;//Guardaremos el dinero que introduce el usuario
            Boolean esCorrecto = false;//Variable para contener errores
            int opcion = 0;//En la que se guarda la opcion elegida por el usuario
            String salida = "";//Dibujaremos la salida para despues del procesamiento mostrarla
            #endregion

            #region Entrada y validacion
            do
            {
                //Limpiamos la pantalla y pintamos el encabezado
                Console.Clear();
                Console.WriteLine(ENCABEZADO);
                //Se repetira dicho proceso mientras que no haya errores
                do
                {
                    
                    dineroIntroducido = leerFloatMayorDeCero();//Leemos por pantalla el dinero que desea introducir el usuario
                    if (dineroIntroducido >= 4.0F&&dineroIntroducido<=50)//Comprobamos que el dinero sea mayor o igual a 4 y menor o igual que 50
                    {
                        esCorrecto = true;
                    }
                    else if (dineroIntroducido < 4.0F)//Comprobamos que el dinero introducido sea menor a 4 Eur
                    {
                        esCorrecto = false;
                        Console.Clear();
                        Console.WriteLine(ENCABEZADO);
                        Console.WriteLine("[ERROR] No se puede introducir una cantidad inferior a 4 Eur\n"
                                        + "Se le devolvera la cantidad introducida.");
                    }
                    else //Comprobamos que el dinero introducido sea mayor a 50 Eur
                    {
                        esCorrecto = false;
                        Console.Clear();
                        Console.WriteLine(ENCABEZADO);
                        Console.WriteLine("[ERROR] No se puede introducir una cantidad superior a 50 Eur\n"
                                        + "Se le devolvera la cantidad introducida.");
                    }
                } while (!esCorrecto);//Se repetira mientras no este correcto
                //Limpiamos la pantalla y volvemos a pintar el encabezado
                Console.Clear();
                Console.WriteLine(ENCABEZADO);
                //Mostramos las opciones de tabaco disponibles y leemos la opcion elegida
                Console.WriteLine("\nListado de cajetillas:");
                opcion = mostrarMenu(nombre, precio);
                //Comprobamos que el dinero introducido sea suficiente para comprar el producto seleccionado
                esCorrecto = (dineroIntroducido < precio[opcion - 1]) ? false : true;
                //Si no es suficiente se le devuelve el dinero y se vuelve a repetir el proceso
                if (dineroIntroducido < precio[opcion - 1])
                {
                    //Pitamos la cabecera y mostramos el error
                    Console.Clear();
                    Console.WriteLine(ENCABEZADO);
                    Console.WriteLine("[ERROR] Cantidad Insuficiente\n"
                                        + "Se le devolvera la cantidad introducida.");
                    Console.WriteLine("Pulsa intro para continuar.");
                    Console.ReadLine();
                }
            }while(!esCorrecto);
#endregion

            #region Proceso
            salida += ENCABEZADO + "\n";//Guardamos el encabezado
            salida += "\nSu Cambio:\n";
            salida += cambio(dineroIntroducido, precio[opcion - 1])+"\n";//Guardamos el cambio necesario a devolver
            salida += "".PadRight(70, '-') + "\n";
            salida += "Aquí tiene su tabaco, gracias\n";//Guardamos el tabaco vendido
            salida += "Su tabaco: " + nombre[opcion - 1]+"\n";
            #endregion

            #region Salida
            //Mostramos todo lo guardado anteriormente
            Console.Clear();
            Console.Write(salida);
            #endregion

        }
        public static int mostrarMenu(String[] nombres, float[] precios)
        {
            int aux = 0;//La utilizaremos para usarla de indice
            int i = 0;
            Boolean esCorrecto = false;
            //Comprobamos que los dos arrays recibidos sean de la misma longitud
            //De no ser asi devuelve -1
            //Si todo va bien continua el programa
            if (nombres.Length != precios.Length)
            {
                aux = -1;
            }
            else
            {
                for (i = 0; i < nombres.Length; i++)
                {
                    //Mostramos por pantalla todos los productos disponibles
                    Console.WriteLine((i + 1) + " " + nombres[i].PadRight(25, '·') + (String)(precios[i] + "").PadRight(6, ' ') + " Eur");
                }
                do
                {
                    //Lee la opcion elegida
                    Console.Write("Selecciona un producto: ");
                    aux = leerEnterosMayorDeCero();
                    //Si la opcion elegida no esta dentro de la lista muestra un error sino, continua
                    esCorrecto = (aux <= nombres.Length) ? true : false;
                    if (!esCorrecto) Console.WriteLine("[ERROR] No ha introducido una opción valida.\n"
                        + "Por favor vuelve a introducir una opción.");
                } while (!esCorrecto);

            }
            //Devuelve la opcion elegida
            return aux;
        }
        public static int leerEnterosMayorDeCero()
        {
            String aux;
            int numero = 0;
            bool esCorrecto = false;
            //Lee un numero entero
            do
            {
                aux = Console.ReadLine();
                esCorrecto = Int32.TryParse(aux, out numero);
                //De no ser correcto muestra un mensaje de error
                if (esCorrecto == false || numero < 0)
                {
                    Console.WriteLine("Error: no ha introducido un número o no es valido.");
                    esCorrecto = false;

                }

            } while (esCorrecto == false);
            //Si todo va bien devuelve el numero introducido
            return numero;

        }
        public static float leerFloatMayorDeCero()
        {
            String aux;
            float numero = 0.0f;
            bool esCorrecto = false;

            do
            {
                //Pide el dinero por pantalla y lo lee
                Console.Write("Por favor, introduce el dinero para comprar el tabaco: ");
                aux = Console.ReadLine();
                //Si no introduce un numero o el numero es menor de 0 muestra un mensaje de error y reinicia el proceso
                esCorrecto = float.TryParse(aux, out numero);
                if (esCorrecto == false || numero < 0)
                {
                    Console.WriteLine("Error: no ha introducido un número o no es valido.");
                    esCorrecto = false;

                }

            } while (esCorrecto == false);
            //Si todo va bien devuelve el numero introducido
            return numero;

        }
        public static string cambio(float dinero, float precio)
        {

            string aux = "";
            String[] nombres = { "Monedas de 0.05", "Monedas de 0.1", "Monedas de 0.2",  "Monedas de 0.5", "Monedas de 1", "Monedas de 2", "Billetes de 5", "Billetes de 10", "Billetes de 20", "Billetes de 50" };
            float[] monedas = { 0.05f, 0.1f, 0.2f, 0.5f, 1.0f, 2.0f, 5.0f, 10.0f, 20.0f, 50.0f };//Las monedas van ordenadas en orden ascendente
            int[] vuelta =new int [monedas.Length];
            int i = 0;
            //Calculamos el dinero a devolver
            dinero = dinero - precio;
            for (i = monedas.Length-1; i >=0; i--)
            {
                //Comprueba que dicha moneda hay que devolverla y la suma para mostrar que cantidad de dicha moneda hay que devolver
                while (dinero >= monedas[i])
                {
                    vuelta[i]++;
                    dinero = dinero - monedas[i];
                }
                //Si es necesario devolver dicho tipo de moneda guarda la moneda y la cantidad
                if(vuelta[i]>0)aux = aux + vuelta[i] + "\t"+nombres[i]+" Eur\n";
            }
            //Devuelve la cantidad de monedas necesarias para dar el cambio
            return aux;
        }
    }
}
