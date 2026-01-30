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

## Prompt 3: Hold Interaction ve Refactoring

**Araç:** ChatGPT-4o
**Tarih/Saat:** 2024-01-30 16:15

**Prompt:**
> Core sistemi 'Hold' (Basılı tutma) mekaniğini destekleyecek şekilde güncellemek istiyorum.
> 1. IInteractable arayüzüne InteractionType ve HoldDuration ekle.
> 2. InteractionDetector scriptini Input.GetKey ile süreyi sayacak şekilde refactor et.
> 3. Bir Chest (Sandık) scripti yaz: Belirli süre basılı tutunca açılsın ve kapak animasyonu oynasın.
> Ludu Arts standartlarına (m_ prefix, region vb.) uymayı unutma.

**Alınan Cevap (Özet):**
> IInteractable arayüzü Enum desteği ile güncellendi. InteractionDetector'a zamanlayıcı ve Progress Bar desteği eklendi. Chest sınıfı oluşturuldu ve diğer scriptler (Door, KeyPickup) yeni arayüze uyarlandı.

**Nasıl Kullandım:**
- [x] Direkt kullandım (Code adaptation required for existing classes)
- [ ] Adapte ettim
- [ ] Reddettim

**Açıklama:**
> Case'in "Must Have" gereksinimi olan farklı etkileşim türlerini desteklemek için sistemi modüler hale getirdim.

## Prompt 4: Switch ve Event Sistemi

**Araç:** ChatGPT-4o
**Tarih/Saat:** 2024-01-30 17:00

**Prompt:**
> "Switch" (Şalter) etkileşimi yapmak istiyorum. Bu şalter modüler olmalı, yani Inspector üzerinden herhangi bir objeyi (Kapıyı açma, Işığı yakma vb.) tetikleyebilmeli.
> 1. UnityEvent kullanan bir Switch scripti yaz.
> 2. Door scriptini bu şalter tarafından tetiklenebilecek (Public metod) hale getir.

**Alınan Cevap (Özet):**
> UnityEvent m_OnActivate ve m_OnDeactivate eventlerini içeren Switch sınıfı sağlandı. Door sınıfındaki ToggleDoor ve Unlock metodları public yapılarak dış erişime açıldı.

**Nasıl Kullandım:**
- [x] Direkt kullandım
- [ ] Adapte ettim
- [ ] Reddettim

**Açıklama:**
> Case'deki "Event-based connection" gereksinimi ve "Chained interactions" bonusunu kapsamak için UnityEvent yapısını tercih ettim.