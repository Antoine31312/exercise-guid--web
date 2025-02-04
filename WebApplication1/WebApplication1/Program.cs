var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseStaticFiles();

app.MapGet("/", () => Results.Content(File.ReadAllText("html/connexion.html"), "text/html"));
app.MapGet("/acceuil", () => Results.Content(File.ReadAllText("html/acceuil.html"), "text/html"));
app.MapGet("/informations", () => Results.Content(File.ReadAllText("html/information.html"), "text/html"));
app.MapGet("/nous_contacter", () => Results.Content(File.ReadAllText("html/ContactPage.html"), "text/html"));
app.MapGet("/a_propos", () => Results.Content(File.ReadAllText("html/a_propos.html"), "text/html"));
app.MapGet("/téléchargé", () => Results.Content(DownloadCsvFile(), "text/csv"));
app.MapPost("/login", HandleLogin);
app.MapGet("/epicerie", () => Results.Content(File.ReadAllText("html/epicerie.html"), "text/html"));
app.MapGet("traitement_epicerie", ProcessCart);



app.Run();

static async Task HandleLogin(HttpContext context)
{
    string systemUserName = "antoine3131";
    string systemPassword = "bob234";

    
    IFormCollection form = await context.Request.ReadFormAsync();
    string nomUtilisateur = form["nom_utilisateur"]!;
    string motDePasse = form["mot_de_passe"]!;
    
    Console.WriteLine($"nom_utilisateur : {nomUtilisateur}");
    Console.WriteLine($"mot_de_passe : {motDePasse}");

    if (nomUtilisateur == systemUserName && motDePasse == systemPassword)
    {
        context.Response.Redirect("/acceuil");
    }
    else
    {
        context.Response.Redirect("/");
    }
}

static IResult ProcessCart(HttpRequest request)
{
    string fruit = request.Query["fruit"];
    int quantite = Convert.ToInt32(request.Query["quantite"]);
    return Results.Ok($"Fruit: {fruit}, Quantite: {quantite} ");
}

    
static string DownloadCsvFile() => 
    "Nom;Email;Telephone\r\n" +
    "Alain;Alain1969@example.com;123-456-7890\r\n" + 
    "Mireille;fleur1@example.com;234-567-8901\r\n"; 