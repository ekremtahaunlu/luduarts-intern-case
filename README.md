Unity Hub'da projeyi açın.

Assets/InteractionSystem/Scenes/SampleScene.unity sahnesini açın.

Play tuşuna basın.

Nasıl Test Edilir
Kontroller
Tuş	Aksiyon
W, A, S, D	Hareket Etme
Mouse	Etrafa Bakma
E (Bas-Çek)	Kapı Açma, Anahtar Toplama, Şalter Çekme
E (Basılı Tut)	Sandık Açma (Hold Interaction)
Test Senaryoları
Door Test (Toggle):

Mavi Kapı'ya (P_Door_Wood) yaklaşın. "Open Door" yazısını görün.

E tuşuna basın, kapı açılacaktır. Tekrar basarsanız kapanır.

Key + Locked Door Test (Inventory):

Kilitli Kapı'ya gidin. "Locked (Red Key Required)" uyarısını görün.

Yerdeki Kırmızı Anahtar'a (P_Key_Red) gidin.

E tuşuna basarak alın. Sol üstteki Inventory listesine "Red Key" eklendiğini doğrulayın.

Kapıya geri dönün, artık açılabilir.

Switch Test (Event System):

Duvardaki Şalter'e (P_Switch_Lever) yaklaşın. "Use Lever" yazısını görün.

E tuşuna basın. Kol aşağı inecek ve uzaktan bağlı olduğu kapının kilidini açacaktır (veya kapıyı açacaktır).

Chest Test (Hold Interaction):

Ahşap Sandığa (P_Chest_Wood) yaklaşın.

E tuşuna basılı tutun.

Ekrandaki yeşil barın dolduğunu izleyin. Dolduğunda kapak yukarı doğru açılacaktır.

Mimari Kararlar
Interaction System Yapısı
Sistem, Interface-based (Arayüz tabanlı) bir mimari üzerine kurulmuştur:

IInteractable: Tüm etkileşimli nesnelerin (Kapı, Sandık, Anahtar) imzaladığı sözleşmedir. Interact(), InteractionType ve HoldDuration özelliklerini barındırır.

InteractionDetector: Sadece IInteractable arayan bir Raycast sistemidir. Karşısındaki objenin ne olduğunu (Kapı mı, Sandık mı?) bilmez, sadece etkileşime girer. Bu sayede sistem Loose Coupling (Gevşek Bağlılık) prensibine uyar.

Neden bu yapıyı seçtim:

Modülerlik ve genişletilebilirlik için. Yeni bir etkileşim türü eklemek istediğimde (örn: NPC) mevcut kodları değiştirmeden sadece IInteractable implemente etmem yeterlidir.

Kullanılan Design Patterns & Techniques
Teknik	Kullanım Yeri	Neden
Interface Pattern	IInteractable	Farklı nesneleri tek bir çatı altında toplamak için.
Observer (UnityEvents)	Switch.cs	Şalterin neyi tetikleyeceğini koddan bağımsız (Inspector'dan) atayabilmek için.
ScriptableObject	KeyItem.cs	Eşya verilerini (Data-driven) yönetmek ve string hatasından kaçınmak için.
Ludu Arts Standartlarına Uyum
C# Coding Conventions
Kural	Uygulandı	Notlar
m_ prefix	[x]	Tüm private serialized field'larda uygulandı.
Region kullanımı	[x]	Fields, Unity Methods, Methods şeklinde ayrıldı.
XML documentation	[x]	Public API ve sınıflar için eklendi.
Silent bypass yok	[x]	Null check'ler yapıldı ve hatalar loglandı.
Naming Convention
Kural	Uygulandı	Örnekler
P_ prefix (Prefab)	[x]	P_Door_Wood, P_Key_Red, P_Switch_Lever
SM_ prefix (Mesh)	[x]	SM_Floor, SM_Key (Visual children)
M_ prefix (Material)	[x]	M_Floor_Dark, M_Door
UI Prefab	[x]	P_UI_InteractionHUD
Prefab Kuralları
Kural	Uygulandı	Notlar
Transform (0,0,0)	[x]	Tüm prefab root'ları sıfır noktasında.
Pivot Points	[x]	Kapı ve Sandık için "Hinge" (Menteşe) parent objeleri oluşturuldu.
Hitbox Size	[x]	Anahtar ve Şalter için Box Collider büyütüldü (UX iyileştirmesi).
Tamamlanan Özellikler
Zorunlu (Must Have)
[x] Core Interaction System (Raycast, Range Check, Interface)

[x] Interaction Types

[x] Instant (Key Pickup)

[x] Hold (Chest - Progress Bar ile)

[x] Toggle (Door, Switch)

[x] Interactable Objects (Door, Key, Switch, Chest)

[x] UI Feedback (Prompt Text, Progress Bar, Inventory List)

[x] Simple Inventory (Key toplama ve UI listeleme)

Ekstra / Bonus (Nice to Have)
[x] UnityEvent Integration: Switch sistemi tamamen event tabanlı yapıldı.

[x] UX Improvement: Küçük objeler (Anahtar, Şalter) için "Pixel Hunting" sorununu önlemek adına fiziksel collider'lar görselden daha büyük ayarlandı.

[x] Procedural Animation: Kapı ve Sandık animasyonları kod tabanlı (Quaternion.Slerp) yumuşak geçişlerle yapıldı.

Dosya Yapısı
Assets/
├── InteractionSystem/
│   ├── Scripts/
│   │   ├── Runtime/
│   │   │   ├── Core/            # IInteractable, KeyItem, InteractionType
│   │   │   ├── Interactables/   # Door, Chest, Switch, KeyPickup
│   │   │   ├── Player/          # InteractionDetector, PlayerInventory, FPSController
│   │   │   └── UI/              # (UI logic embedded in detector/inventory)
│   ├── ScriptableObjects/       # Items (Red Key)
│   ├── Prefabs/
│   │   ├── Interactables/       # P_Door, P_Chest, P_Switch, P_Key
│   │   ├── Player/              # P_Player
│   │   └── UI/                  # P_UI_InteractionHUD
│   └── Scenes/                  # SampleScene
├── Docs/                        # Standart Dokümanlar
├── README.md
└── PROMPTS.md                   # LLM Kullanım Raporu
İletişim
Bilgi	Değer
Ad Soyad	[Adınız Soyadınız]
E-posta	[email@adresiniz.com]
GitHub	[https://www.google.com/search?q=github.com/kullaniciadi]
Bu proje Ludu Arts Unity Developer Intern Case için hazırlanmıştır.
