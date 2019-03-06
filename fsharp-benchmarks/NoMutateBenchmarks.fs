namespace fsharp_benchmarks
    module NoMutateBenchmarks =
        open Godot
        
        let scaleVector2D scalar =
            let v1 = new Vector2(1.0f, 1.0f)
            let v2 = v1 * float32 scalar
            v2.Length()
        
        let scaleVector3D scalar =
            let v1 = new Vector3(1.0f, 1.0f, 1.0f)
            let v2 = v1 * float32 scalar
            v2.Length()
        
        let multiplyVector2D i =
            let v1 = new Vector2(1.0f, 1.0f)
            let v2 = new Vector2(float32 i, float32 i)
            let v3 = v1 * v2
            v3.Length()
            
        let multiplyVector3D i =
            let v1 = new Vector3(1.0f, 1.0f, 1.0f)
            let v2 = new Vector3(float32 i, float32 i, float32 i)
            let v3 =  v1 * v2
            v3.Length()
            
        let translateVector2D i =
            let v1 = new Vector2(1.0f, 1.0f);
            let v2 = new Vector2(float32 i, float32 i)
            let v3 = v1 + v2
            v3.Length()
            
        let translateVector3D i =
            let v1 = new Vector3(1.0f, 1.0f, 1.0f);
            let v2 = new Vector3(float32 i, float32 i, float32 i)
            let v3 = v1 + v2
            v3.Length()
            
        let subtractVector2D i =
            let v1 = new Vector2(float32 i, float32 i)
            let v2 = new Vector2(1.0f, 1.0f)
            let v3 = v1 - v2
            v3.Length()
            
        let subtractVector3D i =
            let v1 = new Vector3(float32 i, float32 i, float32 i)
            let v2 = new Vector3(1.0f, 1.0f, 1.0f)
            let v3 = v1 - v2
            v3.Length()
            
        let lengthVector2D i =
            let v1 = new Vector2(float32 i, float32 i)
            v1.Length()
            
        let lengthVector3D i =
            let v1 = new Vector3(float32 i, float32 i, float32 i)
            v1.Length()
            
        let dotProductVector2D i =
            let v1 = new Vector2(1.0f, 1.0f)
            let v2 = new Vector2(float32 i, float32 i)
            v1.Dot(v2)
            
        let dotProductVector3D i =
            let v1 = new Vector3(1.0f, 1.0f, 1.0f)
            let v2 = new Vector3(float32 i, float32 i, float32 i)
            v1.Dot(v2)