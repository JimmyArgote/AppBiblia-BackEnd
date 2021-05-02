<template>
    <div id="app">
        <Navbar/>
        <router-view></router-view>
    </div>
</template>

<script>
import AppHome from './layouts/AppHome';
import Navbar from './modules/Navbar.vue';

export default {
    components: {AppHome, Navbar},
    name: 'App',
}
</script>