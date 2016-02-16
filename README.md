# Metroapi

## Станции метро в вашем приложении.

Сервис предоставляет сведения о линиях и станциях метрополитена. На сегодня доступны схемы метро Москвы и Санкт-Петербурга.

### Документация | Documentation
 
Список методов и формат ответа api: http://metroapi.ru/swagger/ui/index 


### Пример использования | Usage

```
curl -X GET --header "Accept: application/json" "http://metroapi.ru/api/metro/spb"

curl -X GET --header "Accept: application/json" "http://metroapi.ru/api/metro/moscow"
```
 
### Установка клиента и пример использования | Installation and Usage

```PS
Install-Package MetroApi.Client
```

```C#
var client = new MetroApi.Client.MetroApiClient();
var spbMetroSchema = client.GetSaintPetersburgMetro();
var moscowMetroSchema = client.GetMoscowMetro();
```
