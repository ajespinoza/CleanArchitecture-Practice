using CleanArchitecture.Data;
using CleanArchitecture.Domain;
using Microsoft.EntityFrameworkCore;

StreamerDbContext dbContext = new();

//await AddNewRecords();
//QueryStreaming();
//await QueryFilter();
//await AddNewStreamerWithVideo();
//await AddNewActorWithVideo();
await AddNewDirectorWithVideo();

Console.WriteLine("Presione una tecla para salir");
Console.ReadKey();

async Task AddNewDirectorWithVideo()
{
    var director = new Director { Nombre = "Lorenzo", Apellido = "Basteri", VideoId = 7 };
    await dbContext.AddAsync(director);
    await dbContext.SaveChangesAsync();
}

async Task AddNewActorWithVideo()
{
    var actor = new Actor { Nombre = "Brad", Apellido = "Pitt" };
    await dbContext.AddAsync(actor);
    await dbContext.SaveChangesAsync();

    var videoActor = new VideoActor { ActorId = actor.Id, VideoId = 7 };

    await dbContext.AddAsync(videoActor);
    await dbContext.SaveChangesAsync();
}
async Task AddNewStreamerWithVideo()
{
    var pantalla = new Streamer { Nombre = "Pantalla" };
    var pelicula = new Video { Nombre = "Hunger Games", Streamer = pantalla };

    await dbContext.AddAsync(pelicula);
    await dbContext.SaveChangesAsync(); 
    
}
async Task QueryFilter()
{
    Console.WriteLine($"Ingrese una compañia de streaming:");
    var streamingnombre = Console.ReadLine();

    var streamers = await dbContext.Streamers.Where(x => x.Nombre == streamingnombre).ToListAsync();

    foreach(var streamer in streamers)
    {
        Console.WriteLine($"{streamer.Id} - {streamer.Nombre}");
    }
}


void QueryStreaming()
{
    var streamers = dbContext!.Streamers!.ToList();

    foreach(var streamer in streamers)
    {
        Console.WriteLine($"{streamer.Id} - {streamer.Nombre}");
    }
}

async Task AddNewRecords()
{
    Streamer streamer = new() { Nombre = "Disney", Url = "https://www.disney.com" };
    dbContext!.Streamers!.Add(streamer);
    await dbContext!.SaveChangesAsync();

    var movies = new List<Video>
    {
        new Video
        {
            Nombre = "La Cenicienta",
            StreamerId = streamer.Id
        },
        new Video
        {
            Nombre = "101 Dalmatas",
            StreamerId = streamer.Id
        },
        new Video
        {
            Nombre = "El Jorobado de Notredame",
            StreamerId = streamer.Id
        }
    };


    await dbContext.AddRangeAsync(movies);
    await dbContext.SaveChangesAsync();
}