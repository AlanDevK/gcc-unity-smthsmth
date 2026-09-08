# 1. TRIGGER
- Là một thiết lập của các Collider cho phép chúng có thể kích hoạt một sự kiện khi chạm vào hoặc nằm đè lên GameObject khác.
> Đơn giản là: Vào khu này, một chuyện gì đó sẽ xảy ra.
## Điều kiện hoạt động (giữa hai GameObject)
- Cả hai đều có component `Collider2D`
- Một trong hai GameObject có component `Rigidbody2D` (thông thường trong một game thì Player sẽ có component đó)
## Cách để bật: 
- Vào component `Collider2D` (Loại collider nào cũng có nên gọi chung vậy) và bật `Is Trigger`.
 ![[Pasted image 20260908090424.png]]

## Cách hoạt động:
- Sử dụng các hàm: `OnTriggerEnter2D`, `OnTriggerStay2D`, `OnTriggerExit2D`
---
### OnTriggerEnter2D
> Đơn giản là: Đi vào đây sẽ có sự kiện

```csharp
private void OnTriggerEnter2D(Collider2D other/collider/tên nào cũng được){
	// Đặt code sự kiện bên trong này
}
```

#### VÍ DỤ!!!
> Bạn muốn cho người chơi chạm vào đồng xu rồi đồng xu sẽ biến mất, và khi đồng xu đó được nhặt thì sẽ cộng vào số xu nhặt được? Quá đơn giản.

```csharp
int count = 0;
private void OnTriggerEnter2D(Collider2D other){
	// So sánh xem GameObject chứa Collider2D đang chạm có phải là đồng xu không, sử dụng tag "Collectibles"
	if (other.CompareTag("Collectibles")){
		// Xoá GameObject đó ra khỏi game
		Destroy(other)
		count++;
		Debug.Log($"Da nhat duoc {count} dong xu!");
	}
}
```
---
### OnTriggerStay2D
> Đơn giản là: Bạn ở bên trong khu có sự kiện, sự kiện này sẽ tiếp tục hoạt động cho đến khi bạn rời khỏi nó

```csharp
private void OnTriggerStay2D(Collider2D other/collider/yadayadayada){
	// Đặt code sự kiện trong này
}
```
- OnTriggerStay sẽ chạy một lần theo từng frame hoặc physics update
#### VÍ DỤ!!!
> Bạn muốn tạo một khu vũng lầy (definitely not Dark Souls related) và nó sẽ ăn dần máu của người chơi? Quá đơn giản.

```csharp
// Cắn nó lâu tí :v
[SerializeField] float swampDamage = 0.05f;
[SerializeField] float playerHealth = 100f;
bool isDead;

private void OnTriggerStay2D(Collider2D other){
	// Có nhiều cách để không phải tạo nhiều tag, nhưng hoy làm cách này cho đơn giản :v
	if (other.CompareTag("Poisonous Swamp") && !isDead){
		playerHealth -= swampDamage;
	}
	// Máu của người chơi bằng 0 => Chết haha ngu vcl
	if (playerHealth <= 0){
		isDead = true;
	}
}
```
---
### OnTriggerExit2D
> Giống như `OnTriggerEnter2D`, nhưng ngược lại. Nếu bạn rời khỏi vị trí, sự kiện này sẽ xảy ra

```csharp
private void OnTriggerExit2D(Collider2D other){
	// Đặt code sự kiện trong này
}
```

#### VÍ DỤ!!!
> Nếu bạn rời khỏi vùng an toàn, sẽ có khả năng kẻ địch sẽ đến tấn công bạn

```csharp
bool isProneToEnemyAttack = false;

private void OnTriggerExit2D(Collider2D other){
	if (other.CompareTag("Safe Zone")){
		isProneToEnemyAttack = true;
	}
}
```
---
# 2. RAYCAST
- Là một thuộc tính thuộc `Physics2D`
- Bắn một tia nhỏ từ vị trí chỉ định theo một hướng đến Colliders trong Scene. Những GameObject va chạm với tia Raycast đó đều có thể được khai báo.
- Cách sử dụng: `RaycastHit2D hit = Physics2D.Raycast(origin, direction, distance);`
## VÍ DỤ!
> Bạn cần kiểm tra xem liệu người chơi đang đứng trên đất hay đang nhảy, và bạn không muốn người chơi nhảy vô hạn? Đơn giản.

```csharp
bool isGrounded;
[SerializeField] float jumpForce = 1f;
[SerializeField] float rayLength = 0.5f;
[SerializeField] LayerMask groundLayer;
Rigidbody2D rb;

void Awake(){
	rb = GetComponent<Rigidbody2D>();
}
void Update(){
	bool hit = Physics2D.Raycast(transform.position, Vector2.down, rayLength, groundLayer); 
	// Bắn một ray có thể nhìn thấy được trong Game, màu đỏ, theo hướng xuống
	Debug.DrawRay(transform.position, Vector2.down * rayLength, Color.red);
	if (jumpAction.WasPressThisFrame() && hit){
		rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
	}
}
```
---
## Các loại khác của Raycast
### `RaycastAll`
- Tia Raycast sẽ trả về những GameObject đã va chạm vào tia đó trong một khoảng cách nhất định (có thể một độ dài hoặc khoảng cách từ điểm bắn đến điểm đích sử dụng `Vector2.Distance()`)
```csharp
Physics2D.RaycastAll(origin, direction, distance, layerMask, minDepth, maxDepth)
```
- Giải thích các parameters
	- Vector2 origin: Điểm bắn Ray
	- Vector2 direction: Hướng bắn
	- float distance: Khoảng cách bắn
	- int layerMask: Những layer mà Ray có thể va chạm được
	- float minDepth: Độ sâu tối thiểu, chỉ có thể va chạm với những GameObject có toạ độ z lớn hơn hoặc bằng biến này
	- float maxDepth: Độ sâu tối đa, chỉ có thể va chạm với những GameObject có toạ độ z nhỏ hơn hoặc bằng biến này
### `BoxCast`,`CapsuleCast`,`CircleCast`
- Trả về `true` nếu một Collider đi qua Collider được tạo ra qua những hàm trên
```csharp
Physics2D.BoxCast(origin, size, angle, direction, distance);
Physics2D.CapsuleCast(origin, size, angle, direction, distance);
Physics2D.CircleCast(origin, radius, direction, distance);
```
- Giải thích các parameters:
	- Vector2 size: Kích thước của Collider
	- float angle: Góc quay của Collider (đơn vị: độ ~~mixi~~)
	- Vector2 direction: Hướng của Collider
	- float distance: Khoảng cách của Collider
	- float radius: Bán kính của CircleCollider2D
### `LineCast`
- Trả về `true` nếu một Collider đi qua đoạn thẳng đó
```csharp
Physics2D.LineCast(start, end);
```
### `OverlapArea`
- Trả về Collider đi vào trong khu được chỉ định có hình dạng chữ nhật.
```csharp
Physics2D.OverlapArea(pointA, pointB);
```
- Giải thích các parameters:
	- Vector2 pointA : Điểm một góc của hình chữ nhật
	- Vector2 pointB: Điểm thứ hai của đường chéo hình chữ nhật
### `OverlapBox`, `OverlapCapsule`,`OverlapCircle`
- Trả về Collider đi vào trong khu hình vuông, viên nhộng và tròn được chỉ định
```csharp
Physics2D.OverlapBox(point, size, angle);
Physics2D.OverlapCapsule(point, size, angle);
Physics2D.OverlapCircle(point, radius);
```
- Giải thích các parameters:
	- Vector2 point: Điểm trung tâm của hình
	- float size: Kích thước của Collider
	- float angle: Góc quay của Collider (đơn vị: độ ~~mixi~~)
	- float radius: Bán kính của CircleCollider2D
### `OverlapPoint`
- Trả về Collider mà điểm chỉ định chạm vào
```csharp
Physics2D.OverlapPoint(point);
```
- Vector2 point: Điểm trong không gian game (world space)
### `OverlapCollider`
- Trả về một array Collider va chạm vào Collider được chỉ định
```csharp
Physics2D.OverlapCollider(collider);
```
### `BoxCastAll`, `CapsuleCastAll`, `CircleCastAll`, `OverlapAreaAll`, `OverlapBoxAll`, `OverlapCapsuleAll`, `OverlapCircleAll`, `OverlapPointAll`
- Giống y bản thường, nhưng trả về một array các Collider
---
# 3. LAYERMASK
> Đơn giản là Layer, NHƯNG...

- Biến `LayerMask` sử dụng bitmasks để xác định vị trí của Layer trong Project, và trong các hàm có sử dụng LayerMask thì thường biến phải để là `int`.
## Các cách xử lý!
### 1. LayerMask...?
```csharp
[SerializeField] LayerMask layer;
```
- Khi tạo biến LayerMask và biến có hiện bên trong Inspector của GameObject (đương nhiên phải để public hoặc sử dụng SerializeField), biến sẽ tạo một dropdown để ta có thể chọn được những Layer cho tình huống mà ta đang làm.
### 2. Bit Shift :v
- Bởi vì LayerMask sử dụng bitmasks, nên thường khi đổi sang nhị phân sẽ là một dãy gồm 32 con số 0, vì Unity có thể chứa 32 lớp layer. Ta có thể sử dụng toán tử bitwise để có thể đưa đúng Layer mà ta muốn.
```csharp
// Giả sử Layer mà ta muốn là Layer thứ 9, chúng ta sẽ dịch bit sang trái 9 lần để có thể chọn Layer đó.
int bitmask = 1<<9
```
---
