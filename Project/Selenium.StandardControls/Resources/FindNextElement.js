/**
 * elementの子要素を配列で取得ます
 * @param {HTMLElement} element 親要素
 * @returns {Array<HTMLElement>} 得られた子要素
 */
function getChildren(element) {
  return Array.prototype.slice.call(element.children);
}

/**
 * elementが兄弟の中で何番目かを取得します
 * @param {HTMLElement} element 
 * @returns {Number}
 */
function getIndexInSiblings(element) {
  return getChildren(element.parentElement).indexOf(element);
}

/**
 * sourceと比べて、elementがあとに出現していればtrueを返します
 * @param {HTMLElement} source
 * @param {HTMLElement} element
 * @returns {boolean} 
 */
function isElementAfterMe(source, element) {
  var myIndex = getIndexInSiblings(source);
  var sibling = findSiblingElement(element, source.parentElement);
  if (!sibling) return false;
  var siblingIndex = getIndexInSiblings(sibling);
  return siblingIndex >= myIndex;
}

/**
 * targetがsourceの祖先要素であればtrueを返します
 * @param {HTMLElement} source 
 * @param {HTMLElement} target
 * @returns {boolean}
 */
function isAncestor(source, target) {
  while (source) {
    if (source === target) return true;
    source = source.parentElement;
  }
  return false;
}

/**
 * sourceの祖先要素のなかで、parentを親に持つ要素を返します
 * @param {HTMLElement} source
 * @param {HTMLElement} parent
 * @returns {HTMLElement}
 */
function findSiblingElement(source, parent) {
  while (source && source.parentElement !== parent) {
    source = source.parentElement;
  }
  return source;
}

/**
 * typeとparamを使ってelementの子要素を検索します
 * @param {"id" | "name" | "className" | "cssSelector" | "tagName" | "xpath" | "linkText" | "partialLinkText"} type 
 * @param {string} param 
 * @param {HTMLElement} element 
 * @returns Array<HTMLElement>
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
      return document.evaluate(param, element);
    case "linkText":
      return element.querySelectorAll("*[href='" + param + "']");
    case "partialLinkText":
      return element.querySelectorAll("*[href*='" + param + "']");
  }
}

/**
 * typeとparamを使ってelementより後に出現する最も近い要素を検索します。
 * ただし、element自身と、直系の祖先要素は検索から除外されます。
 * @param {"id" | "name" | "className" | "cssSelector" | "tagName" | "xpath" | "linkText" | "partialLinkText"} type 
 * @param {string} param 
 * @param {HTMLElement} element 
 * @returns {HTMLElement | undefined}
 */
function findNextElement(type, param, element) {
  var source = element;
  while (element) {
    var elements = findNextElements(type, param, element);
    for (var e of elements) {
      if (isAncestor(source, e)) continue;
      if (isElementAfterMe(element, e)) return e;
    }
    element = element.parentElement;
  }
  return null;
}

