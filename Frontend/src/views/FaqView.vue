<template>
    <div>
      <h1>Frequently Asked Questions</h1>
      <div v-for="(faq, index) in faqs" :key="index" class="faq-item">
        <button class="faq-button" @click="toggleFaq(index)">
          {{ faq.question }}
          <span v-if="faq.open">&#9650;</span>
          <span v-else>&#9660;</span>
        </button>
        <div v-if="faq.open" class="faq-answer">{{ faq.answer }}</div>
      </div>
    </div>
  </template>
  
  <script>
  export default {
    name: 'FaqView',
    data() {
      return {
        faqs: [
          {
            question: "What is Vue.js?",
            answer: "Vue.js is a progressive JavaScript framework for building user interfaces."
          },
          {
            question: "How do I get started with Vue?",
            answer: "You can start by including Vue.js in your HTML file and creating a Vue instance."
          },
          // Add more FAQs here
        ]
      };
    },
    methods: {
      toggleFaq(index) {
        this.faqs[index].open = !this.faqs[index].open;
      }
    }
  };
  </script>
  
  <style>
  .faq-item {
    margin-bottom: 20px;
  }
  
  .faq-button {
    background-color: #f0f0f0;
    border: none;
    padding: 10px;
    width: 100%;
    text-align: left;
    cursor: pointer;
    font-weight: bold;
    display: flex;
    justify-content: space-between;
    align-items: center;
  }
  
  .faq-answer {
    padding: 10px;
    border-left: 2px solid #ddd;
    background-color: #f9f9f9;
    margin-top: 5px;
  }
  </style>
  