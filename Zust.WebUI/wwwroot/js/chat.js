"use strict"

// SignalR bağlantısını başlat
var connection = new signalR.HubConnectionBuilder().withUrl("/chathub").build();

// Bağlantı başarılı olunca kullanıcıları getir
connection.start().then(function () {
    GetAllUsers();
})
    .catch(function (err) {
        // Hata oluşursa burada konsola yazdır
        return console.error(err.toString());
    });

// HTML'deki #alert elementini seç
const element = document.querySelector("#alert");

// İlk başta bu elemanı gizli tut
if (element) {
    element.style.display = "none";
}

// Bağlandığında 'Connect' olayını dinle ve mesajı göster
connection.on("Connect", function (info) {
    GetAllUsers();  // Kullanıcıları güncelle
    if (element) {
        element.style.display = "block"; // Uyarı mesajını göster
        element.innerHTML = info; // Gelen bilgiyi uyarı mesajı olarak ayarla
        setTimeout(() => {
            element.style.display = "none"; // 5 saniye sonra gizle
            element.innerHTML = ""; // İçeriği temizle
        }, 5000);
    }
});

// Bağlantı kesildiğinde 'Disconnect' olayını dinle ve mesajı göster
connection.on("Disconnect", function (info) {
    GetAllUsers();  // Kullanıcıları güncelle
    if (element) {
        element.style.display = "block"; // Uyarı mesajını göster
        element.innerHTML = info; // Gelen bilgiyi uyarı mesajı olarak ayarla
        setTimeout(() => {
            element.style.display = "none"; // 5 saniye sonra gizle
            element.innerHTML = ""; // İçeriği temizle
        }, 5000);
    }
});
