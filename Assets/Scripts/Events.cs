using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class Events : MonoBehaviour {
    public event EventHandler OnSpacePressed; //

    private void Start() {
        // OnSpacePressed += Testing_OnSpacePressed;
    }

    // private void Testing_OnSpacePressed(object sender, EventArgs e) {
    //     Debug.Log("Space pressed!");
    // }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            //space pressed
            OnSpacePressed(this, EventArgs.Empty); // executa o evento, pode dar null pointer caso evento ano tenha sido iniciado
            if (OnSpacePressed != null) OnSpacePressed(this, EventArgs.Empty); //mesma coisa linha de cima
            //a partir do c# 6 verificacao so o event nao é nulo 
            OnSpacePressed?.Invoke(this, EventArgs.Empty); // executa evento apenas se nao for nulo
        }
    }
}

public class TestingEventSubscriber : MonoBehaviour {
    private void Start() {
        Events testEvents = GetComponent<Events> ();
        //+= add an event 
        testEvents.OnSpacePressed += Events_OnSpacePressed;
        //-= remove an event
        //testEvents.OnSpacePressed -= Events_OnSpacePressed;
    }

    private void Events_OnSpacePressed(object sender, EventArgs e) {
        Debug.Log("Space pressed");
        //throw new NotImplementedException();
    }
}

public class EventsWithEventArgs : MonoBehaviour {
    public event EventHandler<OnSpacePressedEventArgs> OnSpacePressed;
    public class OnSpacePressedEventArgs : EventArgs {
        public int spaceCount;
    }

    private int spaceCount;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            spaceCount++;
            OnSpacePressed?.Invoke(this, new OnSpacePressedEventArgs { spaceCount = spaceCount} );
        }
    }
}

public class TestingEventSubscriber2 : MonoBehaviour {
    private void Start() {
        EventsWithEventArgs testEvents = GetComponent<EventsWithEventArgs> ();
        testEvents.OnSpacePressed += Events_OnSpacePressed;
    }

    private void Events_OnSpacePressed(object sender, EventsWithEventArgs.OnSpacePressedEventArgs e) {
        Debug.Log("Space pressed: " + e.spaceCount);
    }
}


//Events with delegates
public class EventsWithDelegate : MonoBehaviour {
    
    public event TestEventDelegate OnFloatEvent;
    public delegate void TestEventDelegate(float f);
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            OnFloatEvent?.Invoke(5.5f);
        }
    }
}

public class TestingEventSubscriber3 : MonoBehaviour {
    private void Start() {
        EventsWithDelegate testEvents = GetComponent<EventsWithDelegate> ();
        testEvents.OnFloatEvent += Events_OnFloatEvent;
    }

    private void Events_OnFloatEvent(float e) {
        Debug.Log("Float event: " + e);
    }
}

//Events with delegates
public class EventsWithAction : MonoBehaviour {
    
    public event Action<bool, int> OnActionEvent;
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            OnActionEvent?.Invoke(true, 5);
        }
    }
}

public class TestingEventSubscriber4 : MonoBehaviour {
    private void Start() {
        EventsWithAction testEvents = GetComponent<EventsWithAction> ();
        testEvents.OnActionEvent += Events_OnActionEvent;
    }

    private void Events_OnActionEvent(bool b, int i) {
        Debug.Log("Action: bool " + b + ", int " + i);
    }
}


public class EventsWithUnityEvents : MonoBehaviour {
    public UnityEvent OnUnityEvent;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            OnUnityEvent?.Invoke();
        }
    }
}

public class TestingEventSubscriber5 : MonoBehaviour {
    public void TestingUnityEvent() {
        Debug.Log("Unity Event!");
    }
}