var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Crear una lista para almacenar objetos de tipo Marcas
var marcas = new List<Marca>();

//Configurar una ruta Get para obtener todas las marcas
app.MapGet("/marcas", () =>
{
    return marcas; //Devuelve la lista de clientes
});

//Configurar una ruta Get para obtener una marca especifica por ID
app.MapGet("/marcas/{id}", (int id) =>
{
    //Busca un cliente en la lista que tenga el ID especifico
    var marca = marcas.FirstOrDefault(c => c.Id == id);
    return marca; //Devuelve la marca encontrada (o null si no existe)
});

//Configurar una ruta Post para agregar una nueva marca a la lista 
app.MapPost("/marcas", (Marca marca) =>
{
    marcas.Add(marca); //agregar la nueva marca a la lista 
    return Results.Ok(); //Devuelve una respuesta HTTP 200 ok
});

//Configurar una ruta PUT para actualizar una marca existente por su ID
app.MapPut("/marcas/{id}", (int id, Marca marca) =>
{
    //Busca una marca en la lista que tenga el ID especifico 
    var existingMarca = marcas.FirstOrDefault(c => c.Id == id);
    if (existingMarca != null)
    {
        //Actualiza los datos de la marca existente con los datos proporcionados 
        existingMarca.Name = marca.Name;
        existingMarca.LastName = marca.LastName;
        return Results.Ok(); //Devuelve una respuesta HTTP 200ok 
    }
    else
    {
        return Results.NotFound(); //Devuelve una respuesta HTTP 404 Not Found si la marca no existe 
    }
});

//Configurar una ruta DELETE para eliminar una marca por su ID
app.MapDelete("/marcas/{id}", (int id) =>
{
    //Busca una marca en la lista que tenga el mismo ID
    var existingMarca = marcas.FirstOrDefault(c => c.Id == id);
    if (existingMarca != null)
    {
        //Elimina la marca de la lista 
        marcas.Remove(existingMarca);
        return Results.Ok(); // Devuelve una respuesta 200 ok
    }
    else
    {
        return Results.NotFound(); // Devuelve una respuesta HTTP 404 Not Found si la marca no existe
    }
});

//Ejecutar la ejecucion
app.Run();

// Definicion de la clase Marca que representa la estructura de una marca

internal class Marca
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
}
