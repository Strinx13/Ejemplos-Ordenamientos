//ejemplo de buscar contacto en una linea telefonica

using System;
using System.Collections.Generic;

class Contacto
{
    public string Nombre { get; set; }
    public string Telefono { get; set; }

    public Contacto(string nombre, string telefono)
    {
        Nombre = nombre;
        Telefono = telefono;
    }
}

class Program
{
    static void Main(string[] args)
    {
        bool continuar = true;

        while (continuar)
        {
            List<Contacto> contactos = new List<Contacto>
        {
            new Contacto("Ana", "123-456"),
            new Contacto("Luis", "987-654"),
            new Contacto("Carlos", "456-789"),
            new Contacto("Beatriz", "321-654")
        };

            // Ordenar la lista de contactos usando Selection Sort
            OrdenarContactos(contactos);
            MostrarMenu(contactos);
            // Buscar contacto usando búsqueda binaria 
            Console.WriteLine("ingresa el nombre del contacto a buscar (o presiona Q para salir): ");
            string nombreBuscado = Console.ReadLine();
            //verrificar si el usuario quiere continuar con el programa
            if (nombreBuscado.ToUpper() == "Q")
            {
                continuar = false;
                Console.WriteLine("Saliendo del programa...");
                break;
            }


            int index = BuscarContacto(contactos, nombreBuscado);

            if (index != -1)
            {
                Console.WriteLine($"El número de {nombreBuscado} es: {contactos[index].Telefono}");
            }
            else
            {
                Console.WriteLine($"{nombreBuscado} no fue encontrado.");
            }
            Console.WriteLine("\nPresiona cualquier tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
        }
    }

    // Método para mostrar el menú de contactos
    static void MostrarMenu(List<Contacto> contactos)
    {
        Console.WriteLine("Lista de contactos disponibles:");
        foreach (var contacto in contactos)
        {
            Console.WriteLine($"- {contacto.Nombre}");
        }
        Console.WriteLine();
    }


    // Ordenamiento por selección
    static void OrdenarContactos(List<Contacto> contactos)
    {
        int n = contactos.Count;
        for (int i = 0; i < n - 1; i++)
        {
            int minIndex = i;
            for (int j = i + 1; j < n; j++)
            {
                if (string.Compare(contactos[j].Nombre, contactos[minIndex].Nombre) < 0)
                {
                    minIndex = j;
                }
            }
            // Intercambiar los contactos
            var temp = contactos[minIndex];
            contactos[minIndex] = contactos[i];
            contactos[i] = temp;
        }
    }

    // Búsqueda binaria
    static int BuscarContacto(List<Contacto> contactos, string nombre)
    {
        int izquierda = 0;
        int derecha = contactos.Count - 1;

        while (izquierda <= derecha)
        {
            int medio = (izquierda + derecha) / 2;
            int comparacion = string.Compare(contactos[medio].Nombre, nombre);

            if (comparacion == 0)
            {
                return medio;
            }
            else if (comparacion < 0)
            {
                izquierda = medio + 1;
            }
            else
            {
                derecha = medio - 1;
            }
        }

        return -1; // No encontrado
    }
}
