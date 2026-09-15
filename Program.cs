// Program.cs
// A minimal ASP.NET Core web server for Emmanuel Kimutai's portfolio site.
// It simply serves index.html, index.css, and script.js as static files.
//
// Setup:
//   1. Create a new project:      dotnet new web -n PortfolioSite
//   2. Replace the generated Program.cs with this file.
//   3. Create a "wwwroot" folder inside the project and put
//      index.html, index.css, and script.js inside it.
//   4. Run it:                    dotnet run
//   5. Open the URL shown in the terminal (e.g. http://localhost:5000).

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Serve index.html, index.css, script.js, and any other files in wwwroot/
app.UseDefaultFiles();   // makes "/" automatically serve index.html
app.UseStaticFiles();    // enables serving .css, .js, images, etc.

// Simple endpoint you can call from script.js later if you want a
// dynamic contact form (e.g. fetch('/api/contact', { method: 'POST', ... })).
app.MapPost("/api/contact", (ContactMessage message) =>
{
    if (string.IsNullOrWhiteSpace(message.Name) || string.IsNullOrWhiteSpace(message.Email))
    {
        return Results.BadRequest(new { error = "Name and email are required." });
    }

    // For now this just logs the message to the console.
    // Later you could save it to a database or send an email instead.
    Console.WriteLine($"New contact message from {message.Name} ({message.Email}): {message.Text}");

    return Results.Ok(new { status = "received" });
});

app.Run();

// Simple data shape for the contact form endpoint above.
record ContactMessage(string Name, string Email, string Text);