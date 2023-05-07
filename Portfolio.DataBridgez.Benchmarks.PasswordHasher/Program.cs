using Portfolio.DataBridgez.Benchmarks.PasswordHasher.Benchmarks;

BenchmarkRunner.Run<Pbkdf2PasswordHasherBenchmark>();
BenchmarkRunner.Run<Argon2IdPasswordHasherBenchmark>();