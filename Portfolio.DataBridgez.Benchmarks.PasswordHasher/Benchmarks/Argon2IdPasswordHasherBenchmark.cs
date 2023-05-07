using BenchmarkDotNet.Attributes;
using Microsoft.AspNetCore.Identity;
using Portfolio.DataBridgez.Domain.Entities;
using Portfolio.Databridgez.Infrastructure.Identity;

namespace Portfolio.DataBridgez.Benchmarks.PasswordHasher.Benchmarks;

[MemoryDiagnoser]
public class Argon2IdPasswordHasherBenchmark
{
    private IPasswordHasher<User> PasswordHasher { get; set; } = null!;
    private User User { get; set; } = null!;
    private const string Password = "PasswordSample123$";

    [GlobalSetup]
    public void GlobalSetup()
    {
        PasswordHasher = Activator.CreateInstance<Argon2IdPasswordHasher>();
        User = Activator.CreateInstance<User>();
    }

    [Benchmark]
    public string HashPassword()
    {
        return PasswordHasher.HashPassword(User, Password);
    }
}