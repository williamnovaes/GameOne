using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//System necessario para utilizar ref para tipos de valor
using System;
//Jobs
using Unity.Jobs;
//for NativeArrays
using Unity.Collections;

public class Types {

    //VALUE Types
    //int, float, enum, bool, struct

    //REFERENCE Types
    //class, object, array, string

    //para poder setar null em variaveis de valor
    Nullable<int> variable = null;
    int? variable2 = null;

    public void TesteReferencia() {
    //para passar a referencia de variaveis de valor
        int i = 5;
        Increment(ref i);
    }
    private void Increment(ref int i) {
        i++;
    }
}

//CLASSES sao de REFERENCIA
public class MyClass {
    public int value;

    MyClass(int value) {
        this.value = value;
    }
}

//STRUCTS sao de VALOR
public struct MyStruct {
    public int value;

    public MyStruct(int value) {
        this.value = value;
    }
}

public class TesteJobs {

    public void Testar() {
        //NativeArray passam referencia
        NativeArray<MyStruct> testNativeArray = new NativeArray<MyStruct>(new MyStruct[] {
            new MyStruct(1),
            new MyStruct(2),
            new MyStruct(3)
        }, Allocator.Temp);
        TestJob testJob = new TestJob { testNativeArray = testNativeArray };
        testJob.Run();
    }
}
public struct TestJob : IJob {
    public NativeArray<MyStruct> testNativeArray;
    public void Execute() {
        for (int i = 0; i < testNativeArray.Length; i++) {
            MyStruct myStruct = testNativeArray[i];
            myStruct.value++;
        }
    }
}