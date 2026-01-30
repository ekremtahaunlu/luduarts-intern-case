# LLM Kullanım Dokümantasyonu

> Bu dosya Ludu Arts Unity Developer Intern Case çalışması boyunca yapılan AI destekli geliştirmeleri belgeler.

## Özet

| Bilgi | Değer |
|-------|-------|
| Geliştirici | Ekrem Taha Ünlü |
| Toplam Prompt Sayısı | ~12 (Ana başlıklar altında gruplandı) |
| Kullanılan Araçlar | ChatGPT-4o (Gemini Model) |
| En Çok Yardım Alınan Konular | Mimari Tasarım, C# Standartları (Naming/Coding Conventions), Debugging, Refactoring |
| Tahmini Kazanılan Süre | 4-5 Saat |

---

## Prompt 1: Proje Kurulumu ve Core Sistem

**Araç:** ChatGPT-4o
**Tarih:** 2026-01-30

**Prompt (Özet):**
> Ludu Arts coding convention ve naming convention dokümanlarını paylaşıyorum. Bu standartlara (m_ prefix, region kullanımı, XML documentation vb.) tam uyumlu olacak şekilde; modüler bir IInteractable interface'i ve Raycast tabanlı bir InteractionDetector scripti oluştur.

**Alınan Cevap (Özet):**
> Namespace yapısı düzenlenmiş, serialized field'ları `m_` prefix'i ile başlayan ve XML dokümantasyonları tam olan `IInteractable` ve `InteractionDetector` scriptleri sağlandı.

**Nasıl Kullandım:**
- [x] Direkt kullandım
- [ ] Adapte ettim

**Açıklama:**
> Şirket standartlarına (Coding Conventions.md) ilk seferde hatasız uyum sağlamak ve temiz bir mimari temel atmak için AI desteği aldım.

---

## Prompt 2: Inventory ve Interactable Nesneler

**Araç:** ChatGPT-4o

**Prompt (Özet):**
> Interaction System için 'Inventory' ve 'Door' mekaniklerini kodlamam gerekiyor.
> 1. Anahtarları tanımlamak için ScriptableObject (KeyItem).
> 2. Basit bir PlayerInventory scripti.
> 3. Kilitli/Açık durumları olan bir Door scripti.
> 4. Yerden toplanabilen KeyPickup scripti.

**Alınan Cevap (Özet):**
> ScriptableObject tabanlı veri yapısı, List<KeyItem> tutan bir envanter sistemi ve `IInteractable` arayüzünü implement eden kapı/anahtar sınıfları oluşturuldu.

**Nasıl Kullandım:**
- [x] Direkt kullandım
- [ ] Adapte ettim

**Açıklama:**
> Case gereksinimlerindeki "ScriptableObject ile item tanımları" maddesini karşılamak için veri odaklı bir yapı kurdum.

---

## Prompt 3: Oyuncu Hareketi (FPS Controller)

**Araç:** ChatGPT-4o

**Prompt (Özet):**
> Sahnede hareket edebilmek için CharacterController kullanan, mouse look özelliği olan, Ludu Arts standartlarına uygun basit bir FirstPersonController scripti yazar mısın?

**Alınan Cevap (Özet):**
> `Input.GetAxis` kullanan, kamera açısını clamp'leyen (sınırlayan) ve cursor kilitleme özelliği olan bir kontrolcü scripti sağlandı.

**Nasıl Kullandım:**
- [x] Adapte ettim (Unity Input System ayarlarını "Both" yaparak legacy input sorununu çözdüm)

**Açıklama:**
> Odak noktam Interaction System olduğu için, hareket mekaniğini hızlıca halletmek adına hazır bir yapı istedim.

---

## Prompt 4: Hold Interaction ve Chest Mekaniği

**Araç:** ChatGPT-4o

**Prompt (Özet):**
> Core sistemi 'Hold' (Basılı tutma) mekaniğini destekleyecek şekilde refactor et:
> 1. IInteractable'a InteractionType Enum'ı ekle.
> 2. InteractionDetector'a zamanlayıcı ve Progress Bar UI desteği ekle.
> 3. Belirli süre basılı tutunca açılan bir Chest (Sandık) scripti yaz.

**Alınan Cevap (Özet):**
> Interface güncellendi, Detector scriptine timer mantığı eklendi. Chest scripti için Slerp animasyonu içeren bir yapı sunuldu.

**Nasıl Kullandım:**
- [x] Direkt kullandım
- [ ] Adapte ettim

**Açıklama:**
> Sistemin genişletilebilirliğini test etmek ve "Hold" gereksinimini karşılamak için tüm yapıyı (Interface dahil) güncellettirdim.

---

## Prompt 5: Event Sistemi ve Switch (Şalter)

**Araç:** ChatGPT-4o

**Prompt (Özet):**
> Modüler bir "Switch" (Şalter) yapmak istiyorum. UnityEvent kullanarak Inspector üzerinden herhangi bir objeyi tetikleyebilsin. Door scriptini de buna uygun (Public metotlu) hale getir.

**Alınan Cevap (Özet):**
> `UnityEvent` kullanan Switch sınıfı yazıldı. Door sınıfındaki `ToggleDoor` ve `Unlock` metotları public yapılarak dış erişime açıldı.

**Nasıl Kullandım:**
- [x] Direkt kullandım

**Açıklama:**
> Case'deki "Event-based connection" zorunluluğunu ve modülerlik beklentisini karşılamak için UnityEvent yapısını tercih ettim.

---

## Prompt 6: Debugging ve Polish (Hata Çözümleri)

**Araç:** ChatGPT-4o

**Prompt (Özet):**
> - Kapı animasyonu çalışmıyor (Static hatası çözümü).
> - Chest kapağı kendi etrafında dönüyor, yukarı kalkmıyor (Pivot/Hierarchy çözümü).
> - Anahtarı almak zor oluyor (Hitbox büyütme önerisi).
> - Prefab scale sorunları.

**Alınan Cevap (Özet):**
> Unity editöründe Parent-Child hiyerarşisi kurarak pivot noktası düzeltme, Collider boyutlarını büyütme ve "Static" flag'ini kaldırma gibi çözüm önerileri alındı.

**Nasıl Kullandım:**
- [x] Adapte ettim (Unity Editör üzerinde manuel uyguladım)

**Açıklama:**
> Kodun ötesinde, Unity Editör içindeki komponent ve hiyerarşi hatalarını tespit etmek için LLM'i bir "Pair Programmer" gibi kullandım. Özellikle Chest pivot sorununu kodla değil, hiyerarşi ile çözme önerisi zaman kazandırdı.

---

## Genel Değerlendirme

### LLM'in En Çok Yardımcı Olduğu Alanlar
1.  **Standartlara Uyum:** Ludu Arts'ın belirlediği `m_`, `k_` prefixleri ve XML dokümantasyon kurallarını manuel yazmak yerine AI'a uygulatarak büyük zaman kazandım.
2.  **Refactoring:** Sistemi "Instant" etkileşimden "Hold" destekli yapıya geçirirken yapılan interface değişikliklerini tüm sınıflara (Door, Key, Chest) hızlıca uyguladı.
3.  **Debugging:** Özellikle Inspector tarafındaki hataları (Script'in yanlış objede olması, Collider eksikliği) ekran görüntüleri ve belirtiler üzerinden tespit etmede çok etkili oldu.

### LLM'in Yetersiz Kaldığı Alanlar
1.  **Görsel Hiyerarşi:** Prefab pivot noktası ayarlamaları ve görsel scale sorunlarında kod yazmak yerine Unity Editör tarafında manuel müdahale gerekti. AI sadece yolu gösterdi, uygulamayı ben yaptım.

### Sonuç
LLM araçları, özellikle "Boilerplate" kod yazımında ve şirket standartlarına uyumda süreci çok hızlandırdı. Ancak Unity'nin Component yapısı ve Sahne hiyerarşisi konusundaki problemler yine manuel geliştirici yetkinliği ile çözüldü.

---