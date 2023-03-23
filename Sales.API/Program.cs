using Microsoft.EntityFrameworkCore;
using Sales.API.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer("name=LocalConnection"));
builder.Services.AddTransient<SeedDB>();

var app = builder.Build();

/*IMPORTANTE: SIEMPRE QUE SE COLOQUE EL SEED DB EN UN PROYECTO, HAY QUE BORRAR LA BASE DE DATOS, HAY QUE RECORDAR QUE FUNCIONA COMO
 UN UPDATE-DATABASE EN CODIGO, PORQUE EL SE ASEGURA DE VERIFICAR SI EXISTE O NO LA BASE DE DATOS, Y LA ALIMENTA TAMBIEN CON DATA INICIAL, DESPUES SE CORRE
EL PROYECTO NORMAL*/
SeedData(app);

//se hace de esta manera porque es la unica clase donde no se puede inyectar por constructor
void SeedData(WebApplication app)
{
    IServiceScopeFactory? scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (IServiceScope? scope = scopedFactory!.CreateScope())
    {
        SeedDB? service = scope.ServiceProvider.GetService<SeedDB>();
        service!.SeedAsync().Wait(); //Con Wait es la otra forma de llamar un metodo asincrono, siempre y cuando no se pueda poner un await delante
    }
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//Colocar esta linea siempre para evitar los errores al usar la app Blazor, por la cuestion de los endpoints, y el famoso error del CORS
//aqui cualquier enndpoint recibe lo que sea de cualquier origen, si se quisiera mas seguridad hay que configurarlo luego
app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials());

app.Run();
