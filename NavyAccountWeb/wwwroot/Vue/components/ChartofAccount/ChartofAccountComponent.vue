<template>
    	<!-- WRAPPER -->
    <div>

        <div v-if="errors" class="alert alert-danger alert-dismissible" role="alert">
            <div class="alert-message">
                {{ [errors] }}
            </div>
            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                <span aria-hidden="true">�</span>
            </button>
        </div>
        <div v-if="responseMessage"  class="alert alert-primary alert-dismissible" role="alert"> <div class="alert-message"> {{ responseMessage }} </div> </div>
        <div class="card-body">
            <div v-if="new_actcode">Possible Account Code: <span class="badge badge-pill badge-primary">{{ new_actcode }}</span></div>
            <div class="row">

                <div class="col-12 col-xl-6">
                    <div class="form-group">
                        <label class="form-label">Main Account </label>

                        <select class="form-control" v-model="postBody.mainAccountCode" name="maincode" @change="getLastUsedChartofAccount" required>
                            <option v-for="mainact in mainaccountcodes" v-bind:value="mainact.maincode" v-bind:key="mainact.maincode"> {{ mainact.description }} </option>
                        </select>

                    </div>
                    <div class="form-group">
                        <label class="form-label">Description</label>
                        <input class="form-control" name="description" v-model="postBody.description" placeholder="Description" />
                    </div>
                </div>

                <div class="col-12 col-xl-6">

                    <div class="form-group">
                        <label class="form-label">Sub Type</label>

                        <select class="form-control" v-model="postBody.subtype" required :disabled="autoselectenabled">
                            <option v-for="subt in subtype" v-bind:value="subt.value" v-bind:key="subt.value"> {{ subt.text }} </option>
                        </select>

                    </div>

                    <div class="form-group">
                        <label class="form-label">Balance Sheet</label>

                        <select class="form-control" v-model="postBody.balSheetCode" name="mainAct_dd" required :disabled="autoselectenabled">
                            <option v-for="balSheet in balanceSheetList" v-bind:value="balSheet.bl_code" v-bind:key="balSheet.bl_code"> {{ balSheet.bl_desc }} </option>
                        </select>

                    </div>
                    <div class="btn-group mr-2 sw-btn-group-extra" v-if="canProcess" role="group"><button class="btn btn-submit btn-primary" v-on:click="checkForm" type="submit">{{submitorUpdate}}</button></div>
                </div>

            </div>

        </div>

    </div>
							
	<!-- END WRAPPER -->
</template>

<script>
export default {
    
    data() { 
        return {
        responseMessage:'',
        errors: null,
        submitorUpdate : 'Submit',
        balanceSheetList: null,
        new_actcode:'',
        autoselectenabled:false,
        canProcess : true,
        postBody: {
                acctcode:'',
                ispersonel:false,
                mainAccountCode: '',
                description:'',
                subtype : '',
                balSheetCode:''

        },
        mainaccountcodes:null,
        subtype: [
            { value: '1', text: 'GL Account' },
            { value: '2', text: 'Customers' },
            { value: '3', text: 'Brokers' },
            { value: '4', text: 'Bank' },
            { value: '5', text: 'Insurance Coys' },
            { value: '6', text: 'Mgt Expenses' },
            { value: '7', text: 'Tech Expenses' },
            { value: '8', text: 'Assets' },
            { value: '9', text: 'Income' }
    ]
        };
    },
        mounted() {
         this.$store.state.objectToUpdate = null,
        axios
            .get('/api/MainAccount/getAllMainAccountsDesc')
            .then(response => (this.mainaccountcodes = response.data.data)),
        axios
            .get('/api/BalanceSheet/getAllBalanceSheets')
            .then(response => (this.balanceSheetList = response.data))
      
     },
     
    watch:{
        '$store.state.objectToUpdate':function (newVal, oldval) { 
                this.postBody.mainAccountCode = this.$store.state.objectToUpdate.mainAccountCode,
                this.postBody.description = this.$store.state.objectToUpdate.description
                this.postBody.subtype = this.$store.state.objectToUpdate.subtype,
                this.postBody.balSheetCode = this.$store.state.objectToUpdate.balSheetCode
                this.postBody.acctcode = this.$store.state.objectToUpdate.acctcode
                this.submitorUpdate = 'Update';this.autoselectenabled = true;
               
        }
    },
    methods: {
        checkForm: function (e) {
            
         if (this.postBody.mainAccountCode && this.postBody.subtype && this.postBody.balSheetCode) {
              e.preventDefault();
              this.canProcess = false;
              this.postPost();
          }
          else{

          this.errors = [];
            this.errors.push('Supply the Compulsory filde(s)');
          }
        },
        pad(n,width,z){
            z = z || '0';
            n = n + '';
            return n.length >= width ? n : new Array(width - n.length + 1).join(z) + n;
            },
        getLastUsedChartofAccount() {
            axios.get(`/api/ChartofAccount/getLastChartofAccount/${ this.postBody.mainAccountCode }`)
                        .then(response => {
                            let getSingleMainAct = this.mainaccountcodes.filter(x=>x.maincode == this.postBody.mainAccountCode);
                            
                            if (response.data.responseCode == '200' && response.data.data != 0) {

                                this.new_actcode = 0;
                                this.new_actcode = parseInt(response.data.data.acctcode.split('-')[1]) + 1;
                                this.new_actcode = this.postBody.mainAccountCode + "-"+this.pad(this.new_actcode,4);
                            }
                            else {

                                this.new_actcode = this.postBody.mainAccountCode + "-"+this.pad(1,4);
                            }
                                this.postBody.subtype = getSingleMainAct[0].subtype;
                                this.autoselectenabled = true;
                                this.postBody.balSheetCode = getSingleMainAct[0].npf_balsheet_bl_code;
                        })
                        .catch(e => {
                            this.errors.push(e)
                        });
        },
        postPost() {

            if (this.submitorUpdate == 'Submit') {
                    this.postBody.acctcode = this.new_actcode;
                    axios.post(`/api/ChartofAccount/createChartofAccount`, this.postBody)
                        .then(response => {
                            this.responseMessage = response.data.responseDescription;
                            this.canProcess = true;
                            if(response.data.responseCode == '200'){
                                this.postBody.mainAccountCode = '';this.postBody.description = '';
                                this.postBody.balSheetCode = ''; this.postBody.subtype = '';
                                  this.$store.state.objectToUpdate = "create";
                            }
                        })
                        .catch(e => {
                            this.errors.push(e)
                        });
                }
            if (this.submitorUpdate == 'Update') {
                    axios.put(`/api/ChartofAccount/updateChartofAccount`, this.postBody )
                        .then(response => {
                            this.responseMessage = response.data.responseDescription;
                            this.canProcess = true;
                            if(response.data.responseCode == '200'){
                                 this.submitorUpdate = 'Submit'
                                 this.postBody.mainAccountCode = '';this.postBody.description = '';
                                this.postBody.balSheetCode = ''; this.postBody.subtype = '';
                                  this.$store.state.objectToUpdate = "Update";
                            }
                        })
                        .catch(e => {
                            this.errors.push(e)
                        });
                }
            }
        }
};
</script>