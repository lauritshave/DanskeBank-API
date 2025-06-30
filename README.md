## Danske Bank - Minimalistisk API

Alt kode er på engelsk, alle variabel navne, swagger dokumentation, og kommentarer.
**HUSK:** Swagger er hostet på root i stedet for `/swagger/index.html`.

Kun 1 commit, da det er et mindre project / boilerplate.

### Forudsætninger

- .NET 9 SDK
- Docker

**Kør tests:**
```bash
cd DanskeBank.Tests
dotnet test
```

**Kør docker:**
```bash
cd DanskeBank
docker build -t danskebank .
docker run -d -p 8080:8080 --name danskebank danskebank
```

### Struktur (følger MVC struktur => så vidt muligt)

```
/DanskeBank
  ├─ Controllers/
  ├─ Dtos/
  ├─ Models/
  ├─ Middleware/
  ├─ Repositories/
  ├─ Program.cs
  └─ Dockerfile
```