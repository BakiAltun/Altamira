# Görev

- Uygulama ilk startup aşamasında kullanıcı bilgilerini default password generate ederek tercih ettiğiniz bir database'e eklemeli (Seed aşaması). Bu işlem için jsonplaceholder.typicode.com adresindeki örnek dataları kullanabilirsiniz.

<https://jsonplaceholder.typicode.com/users>

    JSONPlaceholder - Free Fake REST API
    [ { "id": 1, "name": "Leanne Graham", "username": "Bret", "email": "Sincere@april.biz", "address": { "street": "Kulas Light", "suite": "Apt. 556", "city ...


- Kullanıcı bilgileri için CRUD endpointleri bulunmalı.
- Endpointler üzerinde sadece authenticate olan kullanıcılar işlem yapabilmeli.
- API RESTful dizayn edilmeli.
- Yazılan kodların genel kabul görmüş kalite standartlarında olması.
- Kodun, üzerinde geliştirme yapılmaya açık ve anlaşılır olması.
- Inputlar validate edilmeli.
- Ekstra konfigurasyon gerektirmeden (manuel DB kurulumu vs.) kolayca çalışabilir olması.
- Kullanıcı bilgileri redis üzerinde cachlenmeli ve güncelliği sağlanmalı.
- Endpointlerin test edilebilmesi için Swagger entegrasyonu bulunmalı.
- API ile beraber kullanılan tüm teknolojilerin (Database, Redis vb.) docker-compose altında çalıştırılacak şekilde bir .yaml dosyası hazırlanmalı.

OOP, SOLID, Katmanlı Mimari, Design Patterns, TDD gibi yaklaşımların uygulayabileceği metodolojilerin kullanılması artıdır.

**Son teslim tarihi** : 21.07.2021