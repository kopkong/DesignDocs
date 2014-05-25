import time,random,collections

def cross(A, B):
    "Cross product of elements in A and elements in B."
    return [a+b for a in A for b in B]

cols   = '12345'
rows1     = 'DCBA'
squaresA  = cross(rows1, cols)
rows2 = 'EFGH'
squaresB  = cross(rows2,cols) 
types = ['Knight','Footman','Archer','None']

class MyObj:
	""" Simple Data structure to store data"""
	pass

teamA = dict((s,random.choice(types)) for s in squaresA)
teamB = dict((s,random.choice(types)) for s in squaresB)

targetA = dict((s,'') for s in squaresA)
targetB = dict((s,'') for s in squaresB)

blocksA = {1:['A1','A2','B1','B2'],2:['C1','C2','D1','D2'],3:['A3','B3'],4:['C3','D3'],5:['A4','A5','B4','B5'],6:['C4','C5','D4','D5']}
blocksB = {1:['E1','E2','F1','F2'],2:['G1','G2','H1','H2'],3:['E3','F3'],4:['G3','H3'],5:['E4','E5','F4','F5'],6:['G4','G5','H4','H5']}

knightOrders = {1:[2,1,3,4,5,6],2:[1,2,3,4,5,6],3:[1,2,5,6,4,3],4:[1,2,3,4,5,6],5:[6,5,3,4,1,2],6:[5,6,3,4,1,2]}
normalOrders = {1:[1,2,3,4,5,6],2:[1,2,3,4,5,6],3:[3,4,1,5,2,6],4:[1,5,2,6,3,4],5:[5,6,3,4,1,2],6:[5,6,3,4,1,2]}

ObjA = MyObj()
ObjA.name = "Player"
ObjA.team = teamA
ObjA.target = targetA
ObjA.blocks = blocksA

ObjB = MyObj()
ObjB.name = "NPC"
ObjB.team=teamB
ObjB.target = targetB
ObjB.blocks = blocksB

def displayTeam():
	for i in cols:
		line = ''
		for j in rows1 + rows2:
			if j <= 'D':
				line += teamA[j + i] + " " 
			else:
				line += teamB[j + i] + " "

			if j == 'A':
				line += "-------------"
		print line

def displayTarget(objSelf,objTarget):
	print "Display all targets for %s" %(objSelf.name)
	od = collections.OrderedDict(sorted(objSelf.target.items()))
	for i in od:
		if objSelf.team[i] != 'None':
			print "Squad %s(%s) target is %s(%s)" %(i,objSelf.team[i],objSelf.target[i],objTarget.team[objSelf.target[i]])

def searchEmeny(objSelf,objTarget):
	for i in range(1,7):
		# Check teamA blocks is empty or not
		queue = notEmptyBlock(objSelf.blocks[i],objSelf.team)

		if queue:
			for sq in queue:
				if objSelf.target[sq] == '':
					if objSelf.team[sq] == 'Knight':
						#check sq's orders
						for o in knightOrders[i]:
							if searchTarget(sq,objTarget.blocks[o],objTarget.team,objSelf,objTarget):
								break
					else:
						for o in normalOrders[i]:
							if searchTarget(sq,objTarget.blocks[o],objTarget.team,objSelf,objTarget):
								break
				else:
					print ' Already has a rival, stop searching'
		else:
			print "No squads, skip"

def assignTarget(squad,targetSquad,objSelf,objTarget):
	objSelf.target[squad] = targetSquad
	if objTarget.target[targetSquad] == '':
		objTarget.target[targetSquad] = squad


def lessThanTwoMeleeAttacks(objSelf,targetSquad):
	count = sum(1 for x in objSelf.target if objSelf.team[x] != 'Archer' and objSelf.target[x] == targetSquad)
	#print "TargetSquad : %s has %d rivals" %(targetSquad,count)
	return count < 2

def searchTarget(squad,block,rivalTeam,objSelf,objTarget):
	targetBlock = notEmptyBlock(block,rivalTeam)
	#print "Squad " + squad 
	#print "targetBlock :" 
	#print targetBlock
	if targetBlock:
		if len(targetBlock) == 1:
			# If only one squad available
			# It should not have more than two melee rivals at first time
			if lessThanTwoMeleeAttacks(objSelf,targetBlock[0]):
				#objSelf.target[squad] = targetBlock[0]
				assignTarget(squad,targetBlock[0],objSelf,objTarget)
				return True
			else:
				print "This squad has too many rivals, give up"
				return False

		#print targetBlock
		for t in shuffled(targetBlock):
			if objSelf.team[squad] == 'Knight':
				#Knight won't select another knight as first priority
				if objTarget.team[t] == 'Knight':
					print('Skip knights')
				else:
					if lessThanTwoMeleeAttacks(objSelf,t):
						assignTarget(squad,t,objSelf,objTarget)
						return True
			elif objSelf.team[squad] == 'Footman':
				if lessThanTwoMeleeAttacks(objSelf,t):
					assignTarget(squad,t,objSelf,objTarget)
					return True
			else: #Archer
				assignTarget(squad,t,objSelf,objTarget)
				return True
	else:
		print 'No available targets in target block'
		return False

def notEmptyBlock(block,team):
	q = []
	for b in block:
		if team[b] != 'None':
			q.append(b)
	if len(q)>0:
		return q
	else:
		return False

def shuffled(seq):
    "Return a randomly shuffled copy of the input sequence."
    seq = list(seq)
    random.shuffle(seq)
    return seq

if __name__ == '__main__':
	searchEmeny(ObjA,ObjB)
	searchEmeny(ObjB,ObjA)
	displayTarget(ObjA,ObjB)
	displayTarget(ObjB,ObjA)
	displayTeam()



