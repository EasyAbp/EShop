// Copied from https://codepen.io/keelii/pen/RoOzgb
const l = abp.localization.getResource('EasyAbpEShopEShopSample');

var data = JSON.parse(skuInfo)

var res = {}

var spliter = '\u2299'
var r = {}
var keys = []
var selectedCache = []

function combineAttr(data, keys) {
    var allKeys = []
    var result = {}

    for (var i = 0; i < data.length; i++) {
        var item = data[i].skus
        var values = []

        for (var j = 0; j < keys.length; j++) {
            var key = keys[j]
            if (!result[key]) result[key] = []
            if (result[key].indexOf(item[key]) < 0) result[key].push(item[key])
            values.push(item[key])
        }
        allKeys.push({
            path: values.join(spliter),
            sku: data[i].skuId,
            price: data[i].skuPrice,
            currency: data[i].skuCurrency
        })
    }
    return {
        result: result,
        items: allKeys
    }
}


function render(data) {
    var output = ''
    for (var i = 0; i < keys.length; i++) {
        var key = keys[i];
        var items = data[key]

        output += '<dl data-type="' + key + '" data-idx="' + i + '">'
        output += '<dt>' + key + ':</dt>'
        output += '<dd>'
        for (var j = 0; j < items.length; j++) {
            var item = items[j]
            var cName = j == 0 ? 'active' : ''
            if (j == 0) {
                selectedCache.push(item)
            }
            output += '<button data-title="' + item + '" class="' + cName + '" value="' + item + '">' + item + '</button> '
        }
        output += '</dd>'
        output += '</dl>'
    }
    output += '</dl>'

    $('#skuSelector').html(output)
}

function getAllKeys(arr) {
    var result = []
    for (var i = 0; i < arr.length; i++) {
        result.push(arr[i].path)
    }
    return result
}

function powerset(arr) {
    var ps = [[]];
    for (var i = 0; i < arr.length; i++) {
        for (var j = 0, len = ps.length; j < len; j++) {
            ps.push(ps[j].concat(arr[i]));
        }
    }
    return ps;
}

function buildResult(items) {
    var allKeys = getAllKeys(items)

    for (var i = 0; i < allKeys.length; i++) {
        var curr = allKeys[i]
        var sku = items[i].sku
        var price = items[i].price
        var values = curr.split(spliter)

        var allSets = powerset(values)

        for (var j = 0; j < allSets.length; j++) {
            var set = allSets[j]
            var key = set.join(spliter)

            if (res[key]) {
                res[key].sku = items[i].sku
                res[key].price = items[i].price
                res[key].currency = items[i].currency
            } else {
                res[key] = {
                    sku: items[i].sku,
                    price: items[i].price,
                    currency: items[i].currency
                }
            }
        }
    }
}

function trimSpliter(str, spliter) {
    var reLeft = new RegExp('^' + spliter + '+', 'g');
    var reRight = new RegExp(spliter + '+$', 'g');
    var reSpliter = new RegExp(spliter + '+', 'g');
    return str.replace(reLeft, '')
        .replace(reRight, '')
        .replace(reSpliter, spliter)
}

function getSelectedItem() {
    var result = []
    $('dl[data-type]').each(function () {
        var $selected = $(this).find('.active')
        if ($selected.length) {
            result.push($selected.val())
        } else {
            result.push('')
        }
    })

    return result
}

function updateStatus(selected) {
    for (var i = 0; i < keys.length; i++) {
        var key = keys[i];
        var data = r.result[key]
        var hasActive = !!selected[i]
        var copy = selected.slice()

        for (var j = 0; j < data.length; j++) {
            var item = data[j]
            if (selected[i] == item) continue
            copy[i] = item

            var curr = trimSpliter(copy.join(spliter), spliter)
            var $item = $('dl').filter('[data-type="' + key + '"]').find('[value="' + item + '"]')

            var titleStr = '[' + copy.join('-') + ']'

            if (res[curr]) {
                $item.removeClass('disabled')
                setTitle($item.get(0))
            } else {
                $item.addClass('disabled').attr('title', titleStr + ' 无此属性搭配')
            }
        }
    }
}

function handleNormalClick($this) {
    $this.siblings().removeClass('active')
    $this.addClass('active')
}

function handleDisableClick($this) {
    var $currAttr = $this.parents('dl').eq(0)
    var idx = $currAttr.data('idx')
    var type = $currAttr.data('type')
    var value = $this.val()

    $this.removeClass('disabled')
    selectedCache[idx] = value

    console.log(selectedCache)
    $('dl').not($currAttr).find('button').removeClass('active')
    updateStatus(getSelectedItem())

    for (var i = 0; i < keys.length; i++) {
        var item = keys[i]
        var $curr = $('dl[data-type="' + item + '"]')
        if (item == type) continue

        var $lastSelected = $curr.find('button[value="' + selectedCache[i] + '"]')

        if (!$lastSelected.hasClass('disabled')) {
            $lastSelected.addClass('active')
            updateStatus(getSelectedItem())
        }
    }

}

function highLightAttr() {
    for (var i = 0; i < keys.length; i++) {
        var key = keys[i]
        var $curr = $('dl[data-type="' + key + '"]')
        if ($curr.find('.active').length < 1) {
            $curr.addClass('hl')
        } else {
            $curr.removeClass('hl')
        }
    }
}

function bindEvent() {
    $('#skuSelector').undelegate().delegate('button', 'click', function (e) {
        var $this = $(this)

        var isActive = $this.hasClass('.active')
        var isDisable = $this.hasClass('disabled')

        if (!isActive) {
            handleNormalClick($this)

            if (isDisable) {
                handleDisableClick($this)
            } else {
                selectedCache[$this.parents('dl').eq(0).data('idx')] = $this.val()
            }
            updateStatus(getSelectedItem())
            highLightAttr()
            showResult()
        }
    })

    $('button').each(function () {
        var value = $(this).val()

        if (!res[value] && !$(this).hasClass('active')) {
            $(this).addClass('disabled')
        }
    })
}

function showResult() {
    var result = getSelectedItem()
    var s = []

    for (var i = 0; i < result.length; i++) {
        var item = result[i];
        if (!!item) {
            s.push(item)
        }
    }

    if (s.length == keys.length) {
        var curr = res[s.join(spliter)]
        if (curr && curr.sku) {
            cakeProductSkuId = curr.sku
            $('#selectedSku').html('<b>' + l('YourChoice') + '</b><br>' + s.join('\u3000-\u3000'))
            $('#selectedSkuId').html('<b>' + l('SkuId') + '</b><br>' + curr.sku)
            $('#selectedSkuPrice').html('<b>' + l('TotalPrice') + '</b><br>' + curr.price.toFixed(2) + ' ' + curr.currency)
        }
    }
}

function updateData() {
    data = JSON.parse(skuInfo)
    init(data)
}

function setTitle(el) {
    var title = $(el).data('title');
    if (title) $(el).attr('title', title);
}

function setAllTitle() {
    $('#skuSelector').find('button').each(setTitle)
}

function initSkuSelector(data) {
    res = {}
    r = {}
    keys = []
    selectedCache = []

    for (var attr_key in data[0].skus) {
        if (!data[0].skus.hasOwnProperty(attr_key)) continue;
        keys.push(attr_key)
    }
    setAllTitle();

    r = combineAttr(data, keys)

    render(r.result)

    buildResult(r.items)

    updateStatus(getSelectedItem())
    showResult()

    bindEvent()
}

initSkuSelector(data)