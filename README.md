## Danske Bank - Minimalistisk API

Alt kode er på engelsk, herunder alle variabel navne, swagger dokumentation og kommentarer.
**HUSK:** Swagger er hostet på root i stedet for `/swagger/index.html`.

Kun 2 commit, da det er et mindre project / boilerplate.
Hvis i undre jer over hvorfor min GitHub profil ikke har flere commits, er det pga. mine repositories er private :wink:

### Forudsætninger

- .NET 9 SDK
- Docker
- Sørg for port 8080 er tilgængelig (ellers ændre port i kode)

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
Besøg localhost:8080

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