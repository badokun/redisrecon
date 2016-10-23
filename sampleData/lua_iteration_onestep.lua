local function explode(div,str)
    if (div=='') then return false end
    local pos,arr = 0,{}
    for st,sp in function() return string.find(str,div,pos,true) end do
        table.insert(arr,string.sub(str,pos,st-1))
        pos = sp + 1
    end
    table.insert(arr,string.sub(str,pos))
    return arr
end

local sum = 0
local matches = redis.call('smembers', 'urn:battle:k:0:Left')

for _,key in ipairs(matches) do 
	
	--local keyPai = explode('|', key)
	 
	
	--local id = keyPai[1]
	local isMember = redis.call('SISMEMBER', 'urn:battle:k:0:Right', key)
	if isMember == 1 then
		-- redis.call('SADD', 'urn:battle:DONE:0:RightOnly', key)
		redis.call('SREM', 'urn:battle:k:0:Left', key)
		redis.call('SREM', 'urn:battle:k:0:Right', key)
	end
end

--pick smallest left or right

local matches = redis.call('smembers', 'urn:battle:k:0:Left')
for _,key in ipairs(matches) do 
	
	local keyPai = explode('|', key)
	local id = keyPai[1]
	
	-- scan
	-- sscan myset 0 match f*
	local isMember = redis.call('sscan', 'urn:battle:k:0:Right', 0, 'match', id)
	if isMember == 1 then
		-- redis.call('SADD', 'urn:battle:DONE:0:RightOnly', key)
		redis.call('SREM', 'urn:battle:k:0:Left', key)
		redis.call('SREM', 'urn:battle:k:0:Right', key)
	end
end



return sum



