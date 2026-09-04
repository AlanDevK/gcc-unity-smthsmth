# 1. INPUT SYSTEM
- Là hệ thống điều khiển cho phép người chơi điều khiển game hoặc ứng dụng sử dụng các thiết bị và thao tác đa nền tảng.
- Giống như Input Manager, hệ thống này sẽ tiếp nhận thông tin qua những đầu vào trong thư viện InputSystem (của Input Manager là Input). Nhưng những nút điều khiển có thể được cài đặt trong một Input Actions và có thể điều chỉnh cho những trường hợp hành động khác nhau qua các Action Maps
- Đối với những phiên bản Unity cũ, Input System không có sẵn trong engine mà cần phải tải qua Package Manager. Nhưng kể cả khi sử dụng bản mới, khi sử dụng Package Manager cần chỉnh Active Input Handling trong Project Manager -> Player về Both để có thể sử dụng cả hai kiểu hoặc điều chỉnh hẳn về Input System Package (New)
---
## CÁC WORKFLOW PHỔ BIẾN
### Actions
- Developers thiết lập các hành động bên trong Input Actions Editor và cách dễ nhất để truy cập các Action đó trong code là sử dụng `FindAction()`
- Tuỳ vào kiểu loại dữ liệu của Action đó thì sẽ có những hàm khác nhau để truy cập:
	- Vector2: `ReadValue<Vector2>()`
	- Button: `IsPressed()`, `wasPressThisFrame()`, ...
- Hoặc developers có thể sử dụng các hàm Callback được gọi khi Action đó được thực hiện
- Đưa ra giải pháp linh hoạt và đơn giản, phù hợp cho hầu hết tất cả các dự án, tuy nhiên không hỗ trợ có sẵn đối với những dự án nhiều người chơi liên quan đến đa nền tảng.
### Player Input + Actions
- Sử dụng cùng lúc hai Component Player Input và Actions sẽ đưa Input System lên cấp trừu tượng tối đa
- PlayerInput gắn vào trong GameObject muốn được điều khiển, và cho phép Developers có thể gán các hàm liên quan đến các Action được thiết lập bên trong Input Actions Editor vào bên trong component đó
![[Pasted image 20260904143331.png]]
- Developer cũng có thể gán tính năng Rebinding sử dụng `InputActionRebindingExtensions.RebindingOperation`
- Cho phép Developer thiết lập hàm Callback, giúp giảm thiểu độ phức tạp của code nhưng cũng khiến cho việc Debugging phức tạp hơn

### Viết thẳng vào trong code
- Như cái tên, Developer có thể đưa từng nút điều khiển và thao tác trực tiếp vào bên trong code và đọc dữ liệu ngay bên trong đó, nhưng độ linh hoạt không được cao do không có tính trừu tượng giữa code và các giá trị đưa vào, cũng như những thay đổi về điều khiển sẽ phải tuỳ chỉnh trong code.
---
# 2. Physics
- Hệ thống giúp tái tạo vật lý bên trong engine để các object tương tác, tăng tốc và phản ứng với trọng lực, va chạm và các lực khác một cách hợp lý. 
- Đối với 2D, thì Physics2D có thể tái tạo va chạm, Trigger, Raycast...
___
## Collision2D
- Xảy ra khi hai Collider2D va chạm vật lý với nhau
- Xác định hình dạng va chạm của GameObject
- Điều kiện xảy ra va chạm:
	- Cả 2 GameObject đều phải có Collider2D
	- Ít nhất 1 trong 2 GameObject có Rigidbody2D
	- 2 Collider2D nằm trên Layer được phép Collision của nhau
---
### Các Callback phổ biến
#### `OnCollisionEnter2D(Collision2D collision)`
- Được gọi khi bắt đầu va chạm
#### `OnCollisionStay2D(Collision2D collision)`
- Được gọi liên tục khi đang va chạm
#### `OnCollisionExit2D(Collision2D collision)`
- Được gọi khi hai Collider2D ngừng va chạm
---
## Rigidbody2D
- Component vật lý quan trọng bên trong Unity
- Các kiểu loại Rigidbody
	- Static: 
		- Không chịu tác động của trọng lực và lực
		- Chỉ có thể va chạm với một Rigidbody2D Dynamic
		- Khi Collider2D được gán đổi sang trigger, nó sẽ tạo ra trigger với những Collider2D có loại Dynamic hoặc Kinematic
	- Dynamic:
		- Chịu tác động của trọng lực, lực và va chạm
		- Có thể di chuyển bằng `AddForce()` hoặc thay đổi `linearVelocity`
		- Khi Collider2D đổi sang trigger, sẽ tạo ra trigger với những Collider2D được gán với tất cả những Rigidbody2D khác
	- Kinematic:
		- Không chịu tác động của trọng lực và lực vật lý, những Rigidbody2D có loại Static hoặc Kinematic
		- Có thể điều khiển chuyển động trực tiếp
		- Có thể di chuyển sử dụng `Rigidbody2D.velocity` hoặc `Rigidbody2D.angularVelocity`
		- Chỉ có thể va chạm với một Rigidbody2d loại Dynamic
		- Khi Collider2D đổi sang trigger, sẽ tạo ra trigger với những Collider2D được gán với tất cả những Rigidbody2D khác
---
### Các Properties phổ biến:
#### `bodyType`
- Kiểu loại của Rigidbody2D: Static, Dynamic, Kinematic
#### `freezeRotation`
- Ngăn cản không cho GameObject xoay
#### `excludeLayer`
- Chỉ định những Layer mà Collider2D gắn trong Rigidbody2D này nên bỏ qua để chỉ định có tương tác với một Collider2D khác với Layer được chỉ định không
#### `gravityScale`
- Mức độ ảnh hưởng của trọng lực
#### `collisionDetectionMode`
- Xác định cách Rigidbody2D phát hiện va chạm
#### `position`
- Vị trí của Rigidbody đó
#### `rotation`
- Góc xoay của Rigidbody đó
#### `linearVelocity`
- Vận tốc di chuyển của Rigidbody2D
#### `angularVelocity`
- Tốc độ xoay của Rigidbody2D
#### `linearDamping`
- Mức độ giảm vận tốc của Rigidbody2D theo thời gian
___
### Các Methods phổ biến
#### `AddForce()`
- Thêm lực vào Rigidbody2D
#### `MovePosition()`
- Di chuyển Rigidbody2D đến một vị trí chỉ định
#### `MoveRotation()`
- Xoay Rigidbody2D đến một góc nhất định
---
## Trigger
- Phát hiện khi Collider2D tác động vào nó, tuy nhiên không tạo ra va chạm vật lý
- Cách bật: Bật `Is Trigger` trong Collider2D
 ![[Pasted image 20260904155644.png]]
 ---
### Các Callback phổ biến
#### `OnTriggerEnter2D(Collider2D other)`
- Được gọi khi một Collider2D đi vào Trigger
#### `OnTriggerStay2D(Collision2D other)`
- Được gọi liên tục khi Collider2D ở trong Trigger
#### OnTriggerExit2D(Collision2D other)
- Được gọi khi Collider2D rời khỏi Trigger
---
## Raycast2D (Physics2D.Raycast)
- Bắn một tia từ vị trí chỉ định theo một hướng đến Colliders trong Scene. Những GameObject va chạm với tia Raycast đó đều có thể được khai báo.
- Cách sử dụng: `RaycastHit2D hit = Physics2D.Raycast(origin, direction, distance);`
- 
---
## LayerMask
- Dùng để chỉ định Layer nào được phép tương tác với hệ thống
- Thường sử dụng với Raycast, Collision và Physics2D
---
## Các cách di chuyển nhân vật
### 1. `Transform`
`transform.position += Vector3.right * speed * Time.deltaTime;`
- Di chuyển trực tiếp trong Transform
### 2. `Rigidbody2D.linearVelocity`
`rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);`
- Thay đổi trực tiếp vận tốc
### 3. `Rigidbody2D.AddForce()`
`rb.AddForce(Vector2.right * force);`
- Di chuyển bằng lực vật lý
### `4. Rigidbody2D.MovePosition()`
`rb.MovePosition(rb.position + movement);`
- Di chuyển Rigidbody2D đến vị trí mới.