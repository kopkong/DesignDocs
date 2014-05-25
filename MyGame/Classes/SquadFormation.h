//
//  SquadFormation.h
//  MyGame
//
//  Created by 孔 令锴 on 14-5-22.
//
//

#ifndef __MyGame__SquadFormation__
#define __MyGame__SquadFormation__

#include "cocos2d.h"
#include <iostream>
#include <vector>
#include "Squad.h"
USING_NS_CC;

struct Location
{
    FormationRow row;
    FormationColumn col;
    
    Location(FormationRow r, FormationColumn c)
    {
        row = r;
        col = c;
    }
};

enum FormationRow
{
    Row1,
    Row2,
    Row3,
    Row4,
    Row5,
    RowCount
};

enum FormationColumn
{
    Col1,
    Col2,
    Col3,
    Col4,
    ColCount
};

enum Blocks
{
    One,
    Two,
    Three,
    Four,
    Five,
    Six
};

class SquadFormation
{
private:
    Squad* _formation[];
    
public:
    SquadFormation();
    ~SquadFormation();
    void setSquad(int row,int col,Squad*);
    void getSquad(FormationRow,FormationColumn);
    std::vector<Squad*> getBlockSquads(Blocks);
};

#endif /* defined(__MyGame__SquadFormation__) */
