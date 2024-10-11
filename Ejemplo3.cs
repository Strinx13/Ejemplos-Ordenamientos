//Ejemplo de una lista de cosas por hacer
using System;
using System.Collections.Generic;

class Tarea
{
    public string Nombre { get; set; }
    public DateTime FechaLimite { get; set; }

    public Tarea(string nombre, DateTime fechaLimite)
    {
        Nombre = nombre;
        FechaLimite = fechaLimite;
    }
}

class Program
{
    static void Main(string[] args)
    {
        List<Tarea> tareas = new List<Tarea>
        {
            new Tarea("Entregar informe", new DateTime(2024, 11, 15)),
            new Tarea("Revisar proyecto", new DateTime(2024, 10, 20)),
            new Tarea("Tener reunión", new DateTime(2024, 10, 30)),
            new Tarea("Terminar código", new DateTime(2024, 11, 5))
        };

        // Ordenar las tareas por fecha usando Merge Sort
        tareas = OrdenarTareasPorFecha(tareas);

        bool continuar = true;

        while (continuar)
        {
            MostrarMenu(tareas);

            Console.Write("Escribe la fecha de la tarea que deseas buscar (dd/mm/yyyy) o presiona 'Q' para salir: ");
            string inputFecha = Console.ReadLine();

            if (inputFecha.ToUpper() == "Q")
            {
                continuar = false;
                Console.WriteLine("Saliendo del programa...");
                break;
            }

            // Intentar convertir el input a DateTime
            DateTime fechaBuscada;

            if (DateTime.TryParse(inputFecha, out fechaBuscada))
            {
                // Buscar tarea usando búsqueda lineal por fecha
                Tarea tareaEncontrada = BuscarTareaPorFecha(tareas, fechaBuscada);

                if (tareaEncontrada != null)
                {
                    Console.WriteLine($"La tarea para {fechaBuscada.ToShortDateString()} es: {tareaEncontrada.Nombre}");
                }
                else
                {
                    Console.WriteLine($"No hay tareas para la fecha {fechaBuscada.ToShortDateString()}");
                }
            }
            else
            {
                Console.WriteLine("Formato de fecha no válido.");
            }

            Console.WriteLine("\nPresiona cualquier tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
        }
    }

    // Ménu de tareas
    static void MostrarMenu(List<Tarea> tareas)
    {
        Console.WriteLine("Lista de tareas disponibles (ordenadas por fecha límite):");
        foreach (var tarea in tareas)
        {
            Console.WriteLine($"- {tarea.Nombre}: {tarea.FechaLimite.ToShortDateString()}");
        }
        Console.WriteLine();
    }

    // Merge Sort para ordenar tareas por fecha límite
    static List<Tarea> OrdenarTareasPorFecha(List<Tarea> tareas)
    {
        if (tareas.Count <= 1) return tareas;

        int medio = tareas.Count / 2;
        List<Tarea> izquierda = tareas.GetRange(0, medio);
        List<Tarea> derecha = tareas.GetRange(medio, tareas.Count - medio);

        izquierda = OrdenarTareasPorFecha(izquierda);
        derecha = OrdenarTareasPorFecha(derecha);

        return Merge(izquierda, derecha);
    }

    static List<Tarea> Merge(List<Tarea> izquierda, List<Tarea> derecha)
    {
        List<Tarea> resultado = new List<Tarea>();
        int i = 0, j = 0;

        while (i < izquierda.Count && j < derecha.Count)
        {
            if (izquierda[i].FechaLimite <= derecha[j].FechaLimite)
            {
                resultado.Add(izquierda[i]);
                i++;
            }
            else
            {
                resultado.Add(derecha[j]);
                j++;
            }
        }

        resultado.AddRange(izquierda.GetRange(i, izquierda.Count - i));
        resultado.AddRange(derecha.GetRange(j, derecha.Count - j));

        return resultado;
    }

    // Búsqueda lineal por fecha
    static Tarea BuscarTareaPorFecha(List<Tarea> tareas, DateTime fecha)
    {
        foreach (var tarea in tareas)
        {
            if (tarea.FechaLimite == fecha)
            {
                return tarea;
            }
        }

        return null;
    }
}
