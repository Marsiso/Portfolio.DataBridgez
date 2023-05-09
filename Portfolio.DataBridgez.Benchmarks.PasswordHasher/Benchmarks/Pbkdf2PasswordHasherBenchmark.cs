using BenchmarkDotNet.Attributes;
using Microsoft.AspNetCore.Identity;
using Portfolio.DataBridgez.Domain.Entities;
using Portfolio.Databridgez.Infrastructure.Identity;

namespace Portfolio.DataBridgez.Benchmarks.PasswordHasher.Benchmarks;

[MemoryDiagnoser]
public class Pbkdf2PasswordHasherBenchmark
{
    private IPasswordHasher<AppUser> PasswordHasher { get; set; } = null!;
    private AppUser AppUser { get; set; } = null!;
    private const string Password = "PasswordSample123$";

    [GlobalSetup]
    public void GlobalSetup()
    {
        PasswordHasher = Activator.CreateInstance<Pbkdf2PasswordHasher>();
        AppUser = Activator.CreateInstance<AppUser>();
    }

    [Benchmark]
    public string HashPassword()
    {
        return PasswordHasher.HashPassword(AppUser, Password);
    }
}