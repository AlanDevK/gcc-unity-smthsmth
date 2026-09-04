# 1. VÒNG ĐỜI `MonoBehaviour`
## `Awake()`
- Gọi khi Instance của Script được khởi tạo.
- Thường dùng để **khởi tạo dữ liệu nội bộ**.
- Gọi trước `Start()`.
- Mỗi Instance thường chỉ gọi **1 lần**.

---
## `OnEnable()`
- Gọi mỗi khi `MonoBehaviour` được enable.
- Có thể gọi **nhiều lần**.
---
## `Start()`
- Gọi **1 lần** trước frame đầu tiên khi Script được enable.
- Thường dùng khi cần các Object khác đã được khởi tạo.
---
## `Update()`
- Gọi **mỗi frame**.
- Phụ thuộc vào FPS.

---
## `FixedUpdate()`
- Gọi theo **fixed timestep**.
- `Time.fixedDeltaTime` mặc định thường là `0.02s`.

---
## `LateUpdate()`
- Gọi sau khi tất cả `Update()` đã chạy.

---
## `OnDisable()`
- Gọi khi `MonoBehaviour` bị disable.
- Có thể gọi **nhiều lần**.

---
## `OnDestroy()`
- Gọi khi `MonoBehaviour` hoặc GameObject bị hủy.

---
# 2. GIZMOS
**Gizmos** dùng để vẽ các hình ảnh **debug** trong `Scene View`.

---
## `OnDrawGizmos()`
- Vẽ Gizmo trong `Scene View`.
## `OnDrawGizmosSelected()`
- Chỉ vẽ khi GameObject được chọn.
### Một số hàm phổ biến

```
Gizmos.DrawSphere()
Gizmos.DrawWireSphere()

Gizmos.DrawCube()
Gizmos.DrawWireCube()

Gizmos.DrawLine()
Gizmos.DrawRay()

Gizmos.DrawMesh()
Gizmos.DrawIcon()
```

---

# 3. TRANSFORM
`Transform` là Component bắt buộc của mọi GameObject.
Dùng để quản lý:
- Position
- Rotation
- Scale
- Parent / Child

---
## World Space | Local Space

**World Space**  
→ Tương đối với World.

```
transform.position
```

**Local Space**  
→ Tương đối với Parent.

```
transform.localPosition
```

---
## Properties
### Position
```
transform.position
transform.localPosition
```
### Rotation
```
transform.rotation
transform.localRotation
transform.eulerAngles
```
### Scale
```
transform.localScale
```
### Hierarchy
```
transform.parent
transform.childCount
```
### Direction
```
transform.forward
transform.right
transform.up
```

---
## Các hàm phổ biến
### `Translate()`
- Di chuyển Transform.
---
### `Rotate()`
- Xoay Object.
---
### `RotateAround()`
- Xoay quanh một điểm/trục.
---
### `LookAt()`
- Hướng về Target.
---
### `SetParent()`
- Thiết lập Parent.
---
### `GetChild()`
- Lấy Child theo index.
---
### `IsChildOf()`
- Kiểm tra có phải Child hay không.
---
### `Find()`
- Tìm Child theo path/name.
---
### `TransformPoint()`
**Local → World**
```
Vector3 worldPosition =
    transform.TransformPoint(localPosition);
```
### `InverseTransformPoint()`
**World → Local**
```
Vector3 localPosition =
    transform.InverseTransformPoint(worldPosition);
```

---

## Position + Rotation

```csharp
transform.SetPositionAndRotation(
    position,
    rotation
);

transform.SetLocalPositionAndRotation(
    localPosition,
    localRotation
);
```

---
# 4. TIME
- `Time` chứa các thông tin liên quan đến **thời gian trong game**.
---
## `Time.deltaTime`
- Thời gian giữa 2 frame.
→ Giúp chuyển động **không phụ thuộc FPS**.

---
## `Time.fixedDeltaTime`
- Khoảng thời gian giữa các lần `FixedUpdate()`.
---
## `Time.time`
- Thời gian đã trôi qua kể từ khi game bắt đầu.
→ Thường dùng cho Timer.

---
## `Time.timeSinceLevelLoad`
- Thời gian kể từ khi **Scene hiện tại được load**.

---
## `Time.timeScale`
- Điều chỉnh tốc độ thời gian của game.
---
## `Time.unscaledDeltaTime`
- Giống `deltaTime` nhưng **không bị ảnh hưởng bởi `timeScale`**.

---
## `Time.frameCount`
- Số frame đã được render.
---
## `Time.realtimeSinceStartup`

- Thời gian thực kể từ khi Application bắt đầu.
→ Không bị ảnh hưởng bởi `Time.timeScale`.

---
# 5. MATHF
- `Mathf` chứa các hàm toán học thường dùng trong Unity.
---
## `Mathf.Abs()`
Lấy giá trị tuyệt đối.

---

## `Mathf.Min()` / `Mathf.Max()`

Lấy giá trị nhỏ nhất / lớn nhất.

---

## `Mathf.Clamp()`

Giới hạn giá trị trong một khoảng.

```
float result = Mathf.Clamp(value, min, max);
```

---
## `Mathf.Clamp01()`

Giới hạn giá trị từ `0 → 1`.

```
float value = Mathf.Clamp01(input);
```

---

## `Mathf.Lerp()`
**Linear Interpolation** - Nội suy giữa `a` và `b`.

```
float result = Mathf.Lerp(a, b, t);
```

---
## `Mathf.MoveTowards()`
Di chuyển giá trị về Target với giới hạn tốc độ.

```
float value = Mathf.MoveTowards(current,target,maxDelta);
```

---
## `Mathf.SmoothStep()`
Lerp với chuyển động mượt hơn.
```
float value = Mathf.SmoothStep(0f, 1f, t);
```
---
## `Mathf.Round()` / `Floor()` / `Ceil()`

```
Mathf.Round(value)
Mathf.Floor(value)
Mathf.Ceil(value)
```
---
## `Mathf.Sin()` / `Mathf.Cos()`
Hàm lượng giác.
```
Mathf.Sin()
Mathf.Cos()
```
→ Thường dùng cho **chuyển động dạng sóng**.

---
## `Mathf.PI`
- Hằng số **π**.
---
## `Mathf.Deg2Rad` / `Mathf.Rad2Deg`
Đổi giữa **Degree ↔ Radian**.
```
Mathf.Deg2Rad
Mathf.Rad2Deg
```
---
## `Mathf.Repeat()`
Lặp giá trị trong một khoảng.
```
float value = Mathf.Repeat(t, length);
```
---
## `Mathf.PingPong()`
Giá trị chạy qua lại giữa `0` và `length`.
```
float value = Mathf.PingPong(Time.time, 1f);
```
---
## `Mathf.InverseLerp()`
Tìm `t` giữa `a` và `b`.
```
float t = Mathf.InverseLerp(0f, 100f, 50f);
```
→ Ngược chiều với `Lerp()`.