window.axios = require('axios')
window.Vue = require('vue')
import store from './store'
import vuejsDatepicker from 'vuejs-datepicker'
import vuejsAutocomplete from 'vuejs-auto-complete'

/**
 * The following block of code may be used to automatically register your
 * Vue components. It will recursively scan the components directory and
 * automatically register them with their "basename".
 *
 * Eg. ./components/ExampleComponent.vue -> <example-component></example-component>
 */
/////
const files = require.context('./', true, /\.vue$/i)
files.keys().map(key =>
    Vue.component(
        key
            .split('/')
            .pop()
            .split('.')[0],
        files(key).default,
    ),
)

/**
 * You can also comment the code block above and register the components manually
 */
// Vue.component(
//     'example-component',
//     require('./components/ExampleComponent.vue').default,
// )

/**
 * Next, we will create a fresh Vue application instance and attach it to
 * the page. Then, you may begin adding components to this application
 * or customize the JavaScript scaffolding to fit your unique needs.
 */



const app = new Vue({
    el: '#app',
    store
})


