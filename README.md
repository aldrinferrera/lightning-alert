---

### Running the program

```
>dotnet build
>dotnet test
>dotnet run --project LightningAlert
```

---

What is the [time complexity](https://en.wikipedia.org/wiki/Time_complexity) for determining if a strike has occurred for a particular asset?
  - O(n)

If we put this code into production, but found it too slow, or it needed to scale to many more users or more frequent strikes, what are the first things you would think of to speed it up?
  - Check the metrics
    + Create benchmarks, it could be anything from network, configuration and etc
    + Determine if the architecture is still appropriate for scaling up scenarios

---
