# UISystem

UISystem은 UI 프리팹의 생성과 재사용을 관리하는 클래스 입니다. 타입을 키로 인스턴스를 캐싱해 한 번 생성된 UI는 파괴되지 않는 한 재사용되며, 실제 표시/숨김 방식은 `UIBase`를 상속한 각 UI가 `OnOpen()`/`OnClose()`에서 직접 구현합니다. 
프리팹은 코드에서 `RegisterPrefab`으로 등록해야 합니다.

- [🔧 Usage](#-usage)
- [📖 API](#-api)

<br>
<br>


## 🔧 Usage

UIBase 를 상속받는 클래스를 작성합니다.

```cs
public class Inventory : UIBase
```

<br>
<br>

사전에 프리팹을 등록 후 UISystem.Instance 를 통해 접근합니다.


```csharp
UISystem.Instance.RegisterPrefab(inventoryPrefab);

var inventory = UISystem.Instance.Open<Inventory>();

UISystem.Instance.Close<Inventory>();

```

<br>
<br>


## 📖 API

### UISystem

**`RegisterPrefab(UIBase prefab)`** : 프리팹을 타입 기준으로 등록 합니다. 같은 타입을 다시 등록하면 덮어씁니다. <br>
**`Open<T>() where T : UIBase`** : 인스턴스가 없으면 등록된 프리팹으로 생성하고, 있으면 기존 인스턴스를 그대로 사용해 `OnOpen()`을 호출합니다. 프리팹이 등록되어 있지 않으면 `InvalidOperationException`을 던집니다. <br>
**`Close<T>() where T : UIBase`** : 인스턴스가 존재할 때만 `OnClose()`를 호출합니다. 인스턴스 자체를 파괴하거나 등록 해제하지 않습니다. <br>
**`TryGet<T>(out T ui) where T : UIBase`** : 현재 등록된 인스턴스를 열림/닫힘 상태와 무관하게 반환 합니다. <br>
**`Get<T>() where T : UIBase`** : `TryGet`의 단축형. 등록된 인스턴스가 없으면 `null` 반환 합니다. <br>



### UIBase

**`Canvas`** : `Awake()`에서 캐싱되는 컴포넌트. `RequireComponent`로 보장됩니다. <br>
**`IsOpened`** : 현재 열림 상태를 나타내는 추상 프로퍼티. 구현체가 직접 상태를 관리해야 합니다. <br>
**`OnOpen()` / `OnClose()`** : 실제 표시/숨김, 애니메이션 등 열고 닫는 동작을 구현체가 정의하는 추상 메서드. `UISystem`은 호출만 하고 방법은 관여하지 않습니다. <br>

<br>
