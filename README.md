# Metroapi 

Клиент для получения станций метро городов России через [HH API](https://github.com/hhru/api/blob/master/docs/metro.md).

```

## Клиент
 
### Установка

```PS
Install-Package MetroApi.Client
```

### Пример использования

```C#
var client = new MetroApi.Client.MetroApiClient();
var spbMetroSchema = await client.GetSaintPetersburgMetro();
var moscowMetroSchema = await client.GetMoscowMetro();
```
