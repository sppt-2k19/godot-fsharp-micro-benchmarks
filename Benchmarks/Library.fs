namespace Benchmarks

module Bench =
    open System.IO
    open System
    open System.IO
    open Godot

    let scaleVector2D scalar =
        let mutable v1 = new Vector2(1.0f, 1.0f)
        v1 <- v1 * float32 scalar
        v1.Length()
    
    let scaleVector3D scalar =
        let mutable v1 = new Vector3(1.0f, 1.0f, 1.0f)
        v1 <- v1 * float32 scalar
        v1.Length()
    
    let multiplyVector2D i =
        let mutable v1 = new Vector2(1.0f, 1.0f)
        let mutable v2 = new Vector2(float32 i, float32 i)
        v1 <- v1 * v2
        v1.Length()
        
    let multiplyVector3D i =
        let mutable v1 = new Vector3(1.0f, 1.0f, 1.0f)
        let mutable v2 = new Vector3(float32 i, float32 i, float32 i)
        v1 <- v1 * v2
        v1.Length()
        
    let translateVector2D i =
        let mutable v1 = new Vector2(1.0f, 1.0f);
        let v2 = new Vector2(float32 i, float32 i);
        v1 <- v1 + v2
        v1.Length()
        
    let translateVector3D i =
        let mutable v1 = new Vector3(1.0f,1.0f,1.0f);
        let v2 = new Vector3(float32 i, float32 i, float32 i);
        v1 <- v1 + v2
        v1.Length()
        
    let subtractVector2D i =
        let mutable v1 = new Vector2(float32 i,float32 i)
        let v2 = new Vector2(1.0f, 1.0f)
        v1 <- v1 - v2
        v1.Length()
        
    let subtractVector3D i =
        let mutable v1 = new Vector3(float32 i,float32 i,float32 i)
        let v2 = new Vector3(1.0f, 1.0f, 1.0f)
        v1 <- v1 - v2
        v1.Length()
        
    let lengthVector2D i =
        let v1 = new Vector2(float32 i,float32 i)
        v1.Length()
        
    let lengthVector3D i =
        let v1 = new Vector3(float32 i,float32 i,float32 i)
        v1.Length()
        
    let dotProductVector2D i =
        let v1 = new Vector2(1.0f, 1.0f)
        let v2 = new Vector2(float32 i,float32 i)
        v1.Dot v2
        
    let dotProductVector3D i =
        let v1 = new Vector2(1.0f, 1.0f)
        let v2 = new Vector2(float32 i,float32 i)
        v1.Dot v2
        
    let benchmark msg iterations minTime func =
        let mutable count = 1
        let mutable totalCount = 0
        let mutable dummy = 0.0f
        let mutable runningTime = 0.0f
        let mutable deltaTime = 0.0f
        let mutable deltaTimeSquared = 0.0f
        
        while (runningTime < minTime && count < Int32.MaxValue / 2) do
            count <- count * 2
            deltaTime <- 0.0f
            deltaTimeSquared <- 0.0f
            for j = 0 to iterations do
                let timer = System.Diagnostics.Stopwatch.StartNew()
                for i = 0 to count do
                    dummy <- dummy + func i
                runningTime <- float32 timer.ElapsedTicks * 100.0f
                let time = runningTime / (float32 count)
                deltaTime <- deltaTime + time
                deltaTimeSquared <- deltaTime * deltaTime
                totalCount <- totalCount + count
        let mean = deltaTime / float32 iterations
        let standardDeviation = sqrt (deltaTimeSquared - mean * mean * float32 iterations) / float32 (iterations - 1)
        File.AppendAllText("output.txt", String.Format("{0},{1},{2},{3}\n", msg, mean, standardDeviation, count))
        dummy / float32 totalCount

    type BigClass() = 
        inherit Node()
        let mutable testsStarted = false
        
        override this._Ready() =
            GD.Print "Ready from F#" 
            File.WriteAllText("output.txt", "Msg, mean, deviation, count\n")
            
           
           
        override this._Process(delta) =
            if Input.IsActionPressed "ui_select" && not testsStarted then 
                GD.Print "Starting tests"
                testsStarted <- true
                let iterations = 5
                let maxTime = float32 (250 * 1000000)
                let mutable result = 0.0f
                    
                result <- result + benchmark "ScaleVector2D" iterations maxTime scaleVector2D
                result <- result + benchmark "ScaleVector3D" iterations maxTime scaleVector3D
                result <- result + benchmark "MultiplyVector2D" iterations maxTime multiplyVector2D
                result <- result + benchmark "MultiplyVector3D" iterations maxTime multiplyVector3D
                result <- result + benchmark "TranslateVector2D" iterations maxTime translateVector2D
                result <- result + benchmark "TranslateVector3D" iterations maxTime translateVector3D
                result <- result + benchmark "SubtractVector2D" iterations maxTime subtractVector2D
                result <- result + benchmark "SubtractVector3D" iterations maxTime subtractVector3D
                result <- result + benchmark "LengthVector2D" iterations maxTime lengthVector2D
                result <- result + benchmark "LengthVector3D" iterations maxTime lengthVector3D
                result <- result + benchmark "DotProductVector2D" iterations maxTime dotProductVector2D
                result <- result + benchmark "DotProductVector3D" iterations maxTime dotProductVector3D
                
                GD.Print "Done with tests"
