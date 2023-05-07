using BenchmarkDotNet.Attributes;
using Microsoft.AspNetCore.Identity;
using Portfolio.DataBridgez.Domain.Entities;
using Portfolio.Databridgez.Infrastructure.Identity;

namespace Portfolio.DataBridgez.Benchmarks.PasswordHasher.Benchmarks;

[MemoryDiagnoser]
public class Pbkdf2PasswordHasherBenchmark
{
    private IPasswordHasher<User> PasswordHasher { get; set; } = null!;
    private User User { get; set; } = null!;
    private const string Password = "PasswordSample123$";

    [GlobalSetup]
    public void GlobalSetup()
    {
        PasswordHasher = Activator.CreateInstance<Pbkdf2PasswordHasher>();
        User = Activator.CreateInstance<User>();
    }

    [Benchmark]
    public string HashPassword()
    {
        return PasswordHasher.HashPassword(User, Password);
    }
}