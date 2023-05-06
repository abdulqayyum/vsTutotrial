using Microsoft.EntityFrameworkCore;
using vsTutotrial.Data;
namespace vsTutotrial;

public static class ContactEndpoints
{
    public static void MapContactEndpoints (this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/Contact", async (vsTutotrialContext db) =>
        {
            return await db.Contact.ToListAsync();
        })
        .WithName("GetAllContacts")
        .Produces<List<Contact>>(StatusCodes.Status200OK);

        routes.MapGet("/api/Contact/{id}", async (int Id, vsTutotrialContext db) =>
        {
            return await db.Contact.FindAsync(Id)
                is Contact model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetContactById")
        .Produces<Contact>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        routes.MapPut("/api/Contact/{id}", async (int Id, Contact contact, vsTutotrialContext db) =>
        {
            var foundModel = await db.Contact.FindAsync(Id);

            if (foundModel is null)
            {
                return Results.NotFound();
            }

            db.Update(contact);

            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("UpdateContact")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        routes.MapPost("/api/Contact/", async (Contact contact, vsTutotrialContext db) =>
        {
            db.Contact.Add(contact);
            await db.SaveChangesAsync();
            return Results.Created($"/Contacts/{contact.Id}", contact);
        })
        .WithName("CreateContact")
        .Produces<Contact>(StatusCodes.Status201Created);

        routes.MapDelete("/api/Contact/{id}", async (int Id, vsTutotrialContext db) =>
        {
            if (await db.Contact.FindAsync(Id) is Contact contact)
            {
                db.Contact.Remove(contact);
                await db.SaveChangesAsync();
                return Results.Ok(contact);
            }

            return Results.NotFound();
        })
        .WithName("DeleteContact")
        .Produces<Contact>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
