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
        <div v-if="responseMessage" class="alert alert-primary alert-dismissible" role="alert"> <div class="alert-message"> {{ responseMessage }} </div> </div>
        <div class="card-body">
           
            <div class="row">
                <div class="col-xl-6" v-if="wanttoupdate">
                    <div class="form-group">
                        <label class="form-label">Service Number</label>
                        <vuejsAutocomplete source="/api/PersonAPI/getAllPersonsByServiceNoLimited/"
                                           input-class="form-control"
                                           @selected="setValuePersonID"
                                           v-model="postBody.PersonID">
                        </vuejsAutocomplete>

                    </div>

                </div>

            </div>
             <div class="col-12 col-xl-6">
                    <div class="form-group">
                        <label class="form-label">Fund type</label>

                        <select class="form-control" v-model="postBody.fundcode"  required>
                            <option value="0"> All </option>
                            <option v-for="loantype in loantypeList" v-bind:value="loantype.code" v-bind:key="loantype.code"> {{ loantype.description }} </option>
                        </select>

                    </div>
                </div>


        </div>

        <div class="row card-body col-12">

            <!-- <div class="col-4">
                <div class="form-group">
                    <label class="form-label">Aplication Date</label>
                    <vuejsDatepicker input-class="form-control" name="appdate" v-model="postBody.appdate"></vuejsDatepicker>

                </div>

            </div> -->

            <div class="col-4">
                

                <div class="form-group" >
                    <br />
                    <button class="btn btn-primary" v-on:click="generateReport" type="button">{{submitorUpdate}}</button>
                </div>
            </div>


        </div>

    </div>


    <!-- END WRAPPER -->
</template>

<script>
    import vuejsAutocomplete from 'vuejs-auto-complete';
    import vuejsDatepicker from 'vuejs-datepicker';
    import moment from 'moment';

export default {
     props:['fundcode'],
     components: {
         vuejsDatepicker,
         vuejsAutocomplete
        },
    data() {
        return {
           responseMessage:'',
            errors: null,
            wanttoupdate: true,
            svcnoanddisplay: '',
            searchData: '',
            submitorUpdate: 'Submit',
            autoselectenabled: false,
        canProcess : true,
        postBody: {
                  PersonID: '',
                  appdate:'',
                  fundcode:''

        }
        }
        },

        mounted() {
             this.$store.state.objectToUpdate = null,
             this.wanttoupdate = true

              axios
            .get('/api/FundType/getAllFundTypes')
            .then(response => (this.loantypeList = response.data))

            //this.loantypeList.push('ALL');

     },


        methods: {
          
            generateReport() {
                window.open(`/ClaimType/finishClaimRequest/${this.svcnoanddisplay}/${this.postBody.fundcode}`, "_blank"); 
            },
    //         format_date(value){
    //      if (value) {
    //        return moment(String(value)).format('YYYYMMDD')
    //       }
    //   },
 setValuePersonID(result) {
            
                this.svcnoanddisplay = result.display.split('_')
                this.svcnoanddisplay = this.svcnoanddisplay[this.svcnoanddisplay.length - 1];
                this.PersonID = result.value
                this.retrieveDischargeCalc()

            },
        }
};
</script>