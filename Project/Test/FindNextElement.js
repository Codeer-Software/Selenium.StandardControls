/**
 * elementの子要素を配列で取得します
 * @param {HTMLElement} element 親要素
 * @returns {Array<HTMLElement>} 得られた子要素
 */
function getChildren(element) {
  return Array.prototype.slice.call(element.children);
}

/**
 * elementがancestorの中で何番目かを取得します
 * @param {HTMLElement} element 要素
 * @param {HTMLElement} ancestor 祖先要素
 * @returns {Number} 祖先要素の中でのindex
 */
function getIndexInSiblings(source, ancestor) {
  var element = findSiblingElement(source, ancestor)
  return getChildren(ancestor).indexOf(element);
}

/**
 * ルートまでの要素を取得します
 * @param {HTMLElement} source 検索を始める要素
 * @returns  {HTMLElement[]} ルートまでの祖先
 */
function getPath(source) {
  var ret = [source];
  while (source = source.parentElement) {
    ret.push(source);
  }
  return ret;
}

/**
 * element1, element2に共通した祖先を取得します
 * @param {HTMLElement} element1 要素1
 * @param {HTMLElement} element2 要素2
 * @return {HTMLElement} 見つかった祖先要素
 */
function getCommonAncestor(element1, element2) {
  var path1 = getPath(element1);
  var path2 = getPath(element2);
  for (var e of path1) {
    if (path2.includes(e)) {
      return e;
    }
  }
  return null;
}

/**
 * sourceと比べて、elementがあとに出現していればtrueを返します
 * @param {HTMLElement} source 比較元
 * @param {HTMLElement} element 比較する要素
 * @returns {boolean} 後に出現していればtrue
 */
function isElementAfterMe(source, element) {
  var ancestor = getCommonAncestor(source, element);
  if (!ancestor) return false;
  var myIndex = getIndexInSiblings(source, ancestor);
  var siblingIndex = getIndexInSiblings(element, ancestor);
  return siblingIndex >= myIndex;
}

/**
 * targetがsourceの祖先要素であればtrueを返します
 * @param {HTMLElement} source 比較元
 * @param {HTMLElement} target 比較する要素
 * @returns {boolean} targetがsourceの祖先要素であればtrue
 */
function isAncestor(source, target) {
  while (source) {
    if (source === target) return true;
    source = source.parentElement;
  }
  return false;
}

/**
 * parentを親にもつsourceの祖先要素を返します
 * @param {HTMLElement} source 比較元
 * @param {HTMLElement} parent 親要素
 * @returns {HTMLElement} 見つかったHTMLElement
 */
function findSiblingElement(source, parent) {
  while (source && source.parentElement !== parent) {
    source = source.parentElement;
  }
  return source;
}

/**
 * XPathを評価して結果を配列で返します
 * @param {string} xpath 評価するXPath文字列
 * @param {HTMLElement} element 評価の基準となる要素
 * @return {HTMLElement[]} 結果の配列
 */
function evaluateXPath(xpath, element) {
  var ret = [];
  var node = null;
  var result = document.evaluate(xpath, element);
  while (node = result.iterateNext()) {
    ret.push(node);
  }
  return ret;
}

/**
 * typeとparamを使ってelementの子要素を検索します
 * @param {"id" | "name" | "className" | "cssSelector" | "tagName" | "xpath" | "linkText" | "partialLinkText"} type 検索のタイプ
 * @param {string} param 検索パラメータ
 * @param {HTMLElement} element 基準要素
 * @returns {HTMLElement[]} 見つかった要素の配列
 */
function findNextElements(type, param, element) {
  switch (type) {
    case "id":
      return element.querySelectorAll("*[id='" + param + "']");
    case "name":
      return element.querySelectorAll("[name='" + param + "']");
    case "className":
      return element.getElementsByClassName(param);
    case "cssSelector":
      return element.querySelectorAll(param);
    case "tagName":
      return element.getElementsByTagName(param);
    case "xpath":
      return evaluateXPath(param, element);
    case "linkText":
      return evaluateXPath("//a[text()='" + param + "']", element);
    case "partialLinkText":
      return evaluateXPath("//a[contains(text(), '" + param + "')]", element);
  }
}

/**
 * typeとparamを使ってelementより後に出現する最も近い要素を検索します。
 * ただし、element自身と、直系の祖先要素は検索から除外されます。
 * @param {"id" | "name" | "className" | "cssSelector" | "tagName" | "xpath" | "linkText" | "partialLinkText"} type 検索のタイプ
 * @param {string} param 検索パラメータ
 * @param {HTMLElement} element 基準要素
 * @returns {HTMLElement | null} 見つかった要素
 */
function findNextElement(type, param, element) {
  var source = element;
  while (element) {
    var elements = findNextElements(type, param, element);
    for (var e of elements) {
      if (isAncestor(source, e)) continue;
      if (isElementAfterMe(source, e)) return e;
    }
    element = element.parentElement;
  }
  return null;
}
