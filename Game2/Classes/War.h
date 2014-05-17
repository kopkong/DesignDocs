//
//  War.h
//  Game2
//
//  Created by 孔 令锴 on 14-5-16.
//
//

#ifndef __Game2__War__
#define __Game2__War__

#include <iostream>
#include <stdio.h>

class War;
class State
{
public:
    virtual void Prophase(){}
    virtual void Metaphase(){}
    virtual void Anaphase(){}
    virtual void End(){}
    virtual void CurrentState(War* pWar){}
};


class War
{
private:
    State *m_State;
    int m_Days;
    bool m_End;
public:
    War(State* state):m_State(state),m_Days(0),m_End(false){}
    ~War() {delete m_State ;}
    int GetDays() {return m_Days;}
    void SetDays(int days){m_Days = days;}
    void SetState(State *state) { delete m_State; m_State = state;}
    void GetState(){ m_State->CurrentState(this);}
    void SimulateWar(int d){SetDays(d); GetState();}
    void EndWar(){m_End = true;}
    bool IsWarEnd(){return m_End;}
};

class EndState:public State
{
public:
    void End(War* pWar)
    {
        std::cout<<"End war"<<std::endl;
        pWar->EndWar();
    }
    void CurrentState(War* pWar){End(pWar);}
};

class AnaphaseState:public State
{
public:
    void Anaphase(War* pWar)
    {
        if(pWar->GetDays() < 30)
            std::cout<<"第"<<pWar->GetDays()<<"天：战争后期，双方拼死一搏"<<std::endl;
        else
        {
            pWar->SetState(new EndState());
            pWar->GetState();
        }
    }
    
    void CurrentState(War* pWar){Anaphase(pWar);}
};

class MetaphaseState:public State
{
public:
    void Metaphase(War* pWar)
    {
        if(pWar->GetDays() < 20)
            std::cout<<"第"<<pWar->GetDays()<<"天：战争中期，进入相持阶段"<<std::endl;
        else
        {
            pWar->SetState(new AnaphaseState());
            pWar->GetState();
        }
    }
    
    void CurrentState(War* pWar) { Metaphase(pWar);}
};

class ProphaseState:public State
{
public:
    void Prophase(War* pWar)
    {
        if(pWar->GetDays() < 10)
            std::cout<<"第"<<pWar->GetDays()<<"天：战争初期，双方你来我往，互相试探对方"<<std::endl;
        else
        {
            pWar->SetState(new MetaphaseState());
            pWar->GetState();
        }
    }
    
    void CurrentState(War* pWar) { Prophase(pWar);}
};


#endif /* defined(__Game2__War__) */
