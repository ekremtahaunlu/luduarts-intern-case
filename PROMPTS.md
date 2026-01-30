## Prompt 1: Core Interaction System Kurulumu

**Araç:** ChatGPT-4o
**Tarih/Saat:** 2024-01-30 15:00

**Prompt:**
> Ludu Arts coding convention ve naming convention dosyalarını paylaşıyorum. Bu standartlara (m_ prefix, region kullanımı, XML documentation vb.) tam uyumlu olacak şekilde; modüler bir IInteractable interface'i ve Raycast tabanlı bir InteractionDetector scripti yazar mısın?

**Alınan Cevap (Özet):**
> Namespace yapısı ayarlanmış, region'lara bölünmüş ve serialized field'ları m_ prefix ile başlayan InteractionDetector sınıfı ve IInteractable arayüzü sağlandı. Raycast optimizasyonu için interval kontrolü eklendi.

**Nasıl Kullandım:**
- [x] Direkt kullandım
- [ ] Adapte ettim
- [ ] Reddettim

**Açıklama:**
> Temel mimariyi firmaya özgü standartlara (Coding Conventions.md) uygun kurmak için destek aldım. Verilen kodda null check ve XML dokümantasyonları kurallara uygundu.

## Prompt 2: Inventory ve Interactable Nesneler

**Araç:** ChatGPT-4o
**Tarih/Saat:** 2024-01-30 15:30

**Prompt:**
> Interaction System için 'Inventory' ve 'Door' mekaniklerini kodlamam gerekiyor. Ludu Arts standartlarına uygun olarak:
> 1. Anahtarları tanımlamak için bir ScriptableObject (KeyItem).
> 2. Bu anahtarları tutacak basit bir PlayerInventory scripti.
> 3. IInteractable implement eden, kilitli/açık durumları olan bir Door scripti.
> 4. Yerden toplanabilen bir KeyPickup scripti yazar mısın?

**Alınan Cevap (Özet):**
> ScriptableObject tabanlı bir item sistemi, m_CollectedKeys listesi tutan bir PlayerInventory ve IInteractable arayüzünü implement eden Door ile KeyPickup sınıfları oluşturuldu. Door sınıfı kilit kontrolü için envantere erişim mantığı içeriyor.

**Nasıl Kullandım:**
- [x] Direkt kullandım
- [ ] Adapte ettim
- [ ] Reddettim

**Açıklama:**
> Case gereksinimlerinde belirtilen "ScriptableObject ile item tanımları" maddesini karşılamak için veri odaklı bir yapı kurdum. Door scripti hem 'Toggle' hem 'Inventory Check' mantığını içeriyor.