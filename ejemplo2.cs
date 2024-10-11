//Sistema de inventario de productos
using System;
using System.Collections.Generic;

class Producto
{
    public string Nombre { get; set; }
    public decimal Precio { get; set; }

    public Producto(string nombre, decimal precio)
    {
        Nombre = nombre;
        Precio = precio;
    }
}

class Program
{
    static void Main(string[] args)
    {
        List<Producto> productos = new List<Producto>
        {
            new Producto("Laptop", 1000),
            new Producto("Mouse", 25),
            new Producto("Teclado", 50),
            new Producto("Monitor", 300)
        };

        // Ordenar los productos por precio usando Merge Sort
        productos = OrdenarProductos(productos);

        bool continuar = true;

        while (continuar)
        {
            MostrarMenu(productos);

            Console.Write("Escribe el nombre del producto que deseas buscar (o presiona 'Q' para salir): ");
            string nombreBuscado = Console.ReadLine();

            if (nombreBuscado.ToUpper() == "Q")
            {
                continuar = false;
                Console.WriteLine("Saliendo del programa...");
                break;
            }

            // Buscar producto usando búsqueda lineal
            Producto productoEncontrado = BuscarProductoPorNombre(productos, nombreBuscado);

            if (productoEncontrado != null)
            {
                Console.WriteLine($"El precio de {productoEncontrado.Nombre} es: {productoEncontrado.Precio:C}");
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
    //Mostrar Menu
    static void MostrarMenu(List<Producto> productos)
    {
        Console.WriteLine("Lista de productos disponibles (ordenados por precio):");
        foreach (var producto in productos)
        {
            Console.WriteLine($"- {producto.Nombre}: {producto.Precio:C}");
        }
        Console.WriteLine();
    }

    // Merge Sort para ordenar productos por precio
    static List<Producto> OrdenarProductos(List<Producto> productos)
    {
        if (productos.Count <= 1) return productos;

        int medio = productos.Count / 2;
        List<Producto> izquierda = productos.GetRange(0, medio);
        List<Producto> derecha = productos.GetRange(medio, productos.Count - medio);

        izquierda = OrdenarProductos(izquierda);
        derecha = OrdenarProductos(derecha);

        return Merge(izquierda, derecha);
    }

    static List<Producto> Merge(List<Producto> izquierda, List<Producto> derecha)
    {
        List<Producto> resultado = new List<Producto>();
        int i = 0, j = 0;

        while (i < izquierda.Count && j < derecha.Count)
        {
            if (izquierda[i].Precio <= derecha[j].Precio)
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

    // Búsqueda lineal por nombre
    static Producto BuscarProductoPorNombre(List<Producto> productos, string nombre)
    {
        foreach (var producto in productos)
        {
            if (producto.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase))
            {
                return producto;
            }
        }

        return null;
    }
}
