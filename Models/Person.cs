using System;

namespace AvaloniaDIContainer.Models;

public class Person
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; }
    public string Email { get; set; }
    public int Age { get; set; }
}