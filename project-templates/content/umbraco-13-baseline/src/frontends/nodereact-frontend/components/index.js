import HelloWorld from "./HelloWorld";
import LoremIpsum from "./LoremIpsum";

export const components = {
  HelloWorld,
  LoremIpsum,
};

try {
  module.exports = components;
} catch {}
